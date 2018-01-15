using Hologic.AssayDashboard.WebAPI.services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace Hologic.AssayDashboard.WebAPI.Controllers
{
    public class ExtractSSVController : ApiController
    {
        private static readonly log4net.ILog logData = log4net.LogManager
      .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<SSVClass> ssvClassList = new List<SSVClass>();

        // GET: api/ExtractSSV
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ExtractSSV/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ExtractSSV
        public IEnumerable<SSVClass> Post()
        {
           /* lock (ExtractAssayNameController.ControllerSync)
            {
            */
                for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    logData.Info("Get all files from the staged area to extract SSV");
                    HttpPostedFile file = HttpContext.Current.Request.Files.Get(i);
                    string filename = file.FileName;
                    var tempPathToStore = Path.GetTempPath(); 
                    tempPathToStore+="ssv";
                    System.IO.Directory.CreateDirectory(tempPathToStore);
                    //save the file in temp folder
                    file.SaveAs(tempPathToStore + file.FileName);
                    //getting the path of stored file
                    var tempStoredPath = tempPathToStore + file.FileName;
                    var tempfilename = filename.Replace(".pdf", ".txt");
                    string pathToText = System.IO.Path.Combine(tempPathToStore, tempfilename);
                    //Convert PDF file to Text file 
                   // Directory.Delete(tempPathToStore);
                    SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
                    f.OpenPdf(tempStoredPath);
                    if (f.PageCount > 0)
                    {
                        try
                        {
                            string line;
                            int result = f.ToText(pathToText);
                            if (result == 0)
                            {
                                FileStream inFile = new FileStream(pathToText, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                                using (StreamReader sr = new StreamReader(inFile))
                                {
                                    while ((line = sr.ReadLine()) != null)
                                    {

                                        if (line.Contains("SAMPLE CURVE REPORT"))
                                        {
                                            string ssvLineInSample = File.ReadLines(pathToText).Skip(20).Take(1).First();
                                            Debug.WriteLine(ssvLineInSample);
                                            string[] arrSSvSample = ssvLineInSample.Split('.');
                                            SSVClass objectSSV = new SSVClass(Convert.ToInt32(arrSSvSample[0]), Convert.ToInt32(arrSSvSample[1]), Convert.ToInt32(arrSSvSample[2]), Convert.ToInt32(arrSSvSample[3]));
                                            ssvClassList.Add(objectSSV);
                                            break;
                                        }

                                        if (line.Contains("System SW Version:"))
                                        {
                                            string pattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
                                            MatchCollection mc = Regex.Matches(line, pattern);
                                            string[] arr = mc.Cast<Match>().Select(m => m.Value).ToArray();
                                            string[] arrSSv = arr[0].Split('.');
                                            SSVClass objectSSV = new SSVClass(Convert.ToInt32(arrSSv[0]), Convert.ToInt32(arrSSv[1]), Convert.ToInt32(arrSSv[2]), Convert.ToInt32(arrSSv[3]));
                                            ssvClassList.Add(objectSSV);
                                        }
                                    }
                                }
                            }
                        f.ClosePdf();
                    }

                        catch (Exception e)
                        {
                            logData.Error(e.Message);
                        }
                    }
                }
                Debug.WriteLine(ssvClassList);
                return ssvClassList;
          //  }
        }

        // PUT: api/ExtractSSV/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ExtractSSV/5
        public void Delete(int id)
        {
        }
    }
}
