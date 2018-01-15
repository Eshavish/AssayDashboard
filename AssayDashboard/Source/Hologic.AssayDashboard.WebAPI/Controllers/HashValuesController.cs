using Hologic.AssayDashboard.Database.Models;
using Hologic.AssayDashboard.Database.Repositories;
using Hologic.AssayDashboard.WebAPI.services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http;

namespace Hologic.AssayDashboard.WebAPI.Controllers
{
    public class HashValuesController : ApiController
    {
        private static readonly log4net.ILog logData = log4net.LogManager
        .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: api/HashValues
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/HashValues/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/HashValues
        public IEnumerable<int> Post()
        {
            List<int> exists = new List<int>();
            List<string> hashedValues = new List<string>();
            for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
               
                List<string> hashValues = new List<string>();
                HttpPostedFile file = HttpContext.Current.Request.Files.Get(i);
                string filename = file.FileName;
                Debug.WriteLine(file);
                Debug.WriteLine(filename);
                using (var repo = new AssayRepository())
                {
                    try
                    {
                        var fileStream = file.InputStream;
                        SHA1Managed sha = new SHA1Managed();
                        byte[] checksum = sha.ComputeHash(fileStream);
                        string sendCheckSum = BitConverter.ToString(checksum)
                            .Replace("-", string.Empty);                    

                        var hashValuesObj = repo.GetReport();
                        foreach (var hashtype in hashValuesObj)
                        {                        
                            hashedValues.Add(hashtype.HashValue);
                        }
                        Debug.WriteLine(hashedValues);
                        if (hashedValues.Contains(sendCheckSum))
                        {
                            exists.Add(1);
                        }
                        else
                        {
                            exists.Add(0);
                        }
                        Debug.WriteLine(exists);
                       // return exists;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        logData.Error(e.Message);
                        return null;
                    }
                }

              //  return exists;
            }
                return exists;
        }

        // DELETE: api/HashValues/5
        public void Delete(int id)
        {
        }
    }
}
