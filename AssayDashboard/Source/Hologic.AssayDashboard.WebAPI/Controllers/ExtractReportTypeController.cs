
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Text;
using System.IO;
using System.Diagnostics;
using Hologic.AssayDashboard.WebAPI.Controllers;

namespace Hologic.AssayDashboard.WebApi.Controllers
{


    public class ExtractReportTypeController : ApiController
    {
        private static readonly log4net.ILog logData = log4net.LogManager
        .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: api/ExtractFileData
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ExtractFileData/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ExtractReportType

        public IEnumerable<string> Post()
        {
            /*lock (ExtractAssayNameController.ControllerSync)
            { */
                List<string> reportTypes = new List<string>();
                for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    logData.Info("Get all files from the staged area");
                    HttpPostedFile file = HttpContext.Current.Request.Files.Get(i);
                    string filename = file.FileName;
                    var tempPathToStore = Path.GetTempPath();
                    tempPathToStore += "reporttypes";
                    System.IO.Directory.CreateDirectory(tempPathToStore);
                    //save the file in temp folder
                    file.SaveAs(tempPathToStore + file.FileName);
                    //getting the path of stored file
                    var tempStoredPath = tempPathToStore + file.FileName;
                    var tempfilename = filename.Replace(".pdf", ".txt");
                    string pathToText = System.IO.Path.Combine(tempPathToStore, tempfilename);
                    //Convert PDF file to Text file 
                    SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
                    f.OpenPdf(tempStoredPath);
                    if (f.PageCount >= 0)
                    {
                        try
                        {
                            int result = f.ToText(pathToText);
                            if (result == 0)
                            {
                                FileStream inFile = new FileStream(pathToText, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                                using (var sr = new StreamReader(inFile))
                                {
                                    while (!sr.EndOfStream)
                                    {
                                        var line = sr.ReadLine();
                                        if (String.IsNullOrEmpty(line)) continue;
                                        if (line.IndexOf("RESULTS REPORT", StringComparison.CurrentCultureIgnoreCase) >= 0)
                                        {
                                            reportTypes.Add("ResultReport");
                                            break;
                                        }
                                        else
                                            if (line.IndexOf("PREVALENCE", StringComparison.CurrentCultureIgnoreCase) >= 0)
                                        {
                                            reportTypes.Add("PrevalenceReport");
                                            break;
                                        }
                                        else
                                             if (line.IndexOf("LEVEY-JENNINGS", StringComparison.CurrentCultureIgnoreCase) >= 0)
                                        {
                                            reportTypes.Add("QCReport");
                                            break;
                                        }
                                        else
                                            if (line.IndexOf("SAMPLE", StringComparison.CurrentCultureIgnoreCase) >= 0)
                                        {
                                            reportTypes.Add("SampleCurveReport");
                                            break;
                                        }
                                        else
                                         if (line.IndexOf("RESULTS BY WORKLIST REPORT", StringComparison.CurrentCultureIgnoreCase) >= 0)
                                        {
                                            reportTypes.Add("ResultWorklistReport");
                                            break;
                                        }
                                        else
                                        {
                                            reportTypes.Add("Choose ReportType");
                                            break;
                                        }
                                    }
                                }

                            }
                            logData.Info("Returning the Result Types");
                        }
                        //extracting assay name ends
                        catch (Exception e)
                        {
                            logData.Error(e.Message);
                            Debug.WriteLine(e.Message);
                        }
                    f.ClosePdf();
                }
                    else
                    {
                        reportTypes.Add("Choose ReportType");
                    }
                }
                return reportTypes;
          //  }
        }

        // PUT: api/ExtractFileData/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ExtractFileData/5
        public void Delete(int id)
        {
        }
    }
}
