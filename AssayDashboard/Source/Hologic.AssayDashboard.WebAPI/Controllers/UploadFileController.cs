using Hologic.AssayDashboard.Database.repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http;
using WebGrease;

namespace Hologic.AssayDashboard.WebApi.Controllers
{
    public class UploadFileController : ApiController
    {
        private static readonly log4net.ILog logData = log4net.LogManager
     .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: api/UploadFile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UploadFile/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UploadFile
        public HttpResponseMessage Post()
        {

            logData.Info("Uploading all files started");
            try
            {
                for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files.Get(i);
                    var fileJsonMeta = HttpContext.Current.Request[$"file-metadata-{i}"];
                    Debug.WriteLine(fileJsonMeta);             
                    using (var memoryStream = new MemoryStream())
                    {
                        
                        bool fileExists = false;                  
                        var fileStream = file.InputStream;
                        SHA1Managed sha = new SHA1Managed();
                        byte[] checksum = sha.ComputeHash(fileStream);
                        string sendCheckSum = BitConverter.ToString(checksum)
                            .Replace("-", string.Empty);
                       /* using(var repo= new ReportRepoClass())
                        {
                            repo.checkIfFileExists(fileJsonMeta, sendCheckSum);
                        }*/
                       
                        fileStream.Position = 0;
                        fileStream.CopyTo(memoryStream);
                        var serializedFile = memoryStream.ToArray();
                        Debug.WriteLine(serializedFile);
                        using (var repo = new ReportRepoClass())
                        {
                            // sending the report and metadata to the database.
                            repo.sendToReportFile(serializedFile, fileJsonMeta, sendCheckSum);
                        }
                    }



                }
                logData.Info("Uploading all files ending");
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                logData.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        // PUT: api/UploadFile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UploadFile/5
        public void Delete(int id)
        {
        }
    }
}
