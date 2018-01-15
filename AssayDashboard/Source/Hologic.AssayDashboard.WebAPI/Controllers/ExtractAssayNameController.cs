using Hologic.AssayDashboard.Database.Repositories;
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

    public class ExtractAssayNameController : ApiController
    {
        public static object ControllerSync = new object();

        private static readonly log4net.ILog logData = log4net.LogManager
       .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: api/ExtractAssayName
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ExtractAssayName/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ExtractAssayName
        public IEnumerable<string> Post()
        {
          /*  lock (ControllerSync)
            {
            */
                List<string> assayNames = new List<string>();
                for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    int flag = 0;
                    logData.Info("Get all files from the staged area to extract assayname");
                    HttpPostedFile file = HttpContext.Current.Request.Files.Get(i);
                    string filename = file.FileName;
                    var tempPathToStore = Path.GetTempPath();
                    tempPathToStore += "assaynames";
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
                    if (f.PageCount > 0)
                    {
                        int count = 0;
                        try
                        {
                            string line;
                            //getting assaynames from database
                            var repo = new AssayRepository();
                            List<string> assaylist1 = new List<string>();
                            //string[] assayFromDb;
                            var assaynamesobj = repo.GetAssay();
                            foreach (var assaytype in assaynamesobj)
                            {
                                string assayname = assaytype.AssayShortName;
                                assaylist1.Add(assayname);
                            }
                            string[] assaylistFromDB = assaylist1.ToArray();
                            foreach(var ind in assaylistFromDB)
                            {
                                Debug.WriteLine(ind);
                            }
                            //string[] assaylist1 = { "AC2", "AC2 250  (1)", "AdV/hMPV/RV", "Babesia", "C. diff", "CT", "CT 250", "DENV", "dHBV", "dHCV", "dHIV", "DNA60", "Flu A/B/RSV", "GBS", "GC", "GC 250  (6)", "GT HPV", "HEV", "HPV", "HPV 250  (14)", "HSV 1&2", "LDT RT TMA-1", "LDT RT TMA-2", "LDT RT TMA-3", "LDT TMA-1", "LDT TMA-2", "LDT TMA-3", "LDT1", "M gen", "MRSA", "OADNA55", "OADNA58", "OADNA62", "OADNA65", "Paraflu", "Parvo/HAV", "PSA 100", "QCDNAIC", "QCFluPPR", "QCRNAIC", "qHBV", "qHCV", "qHIV-1", "RNA60", "RNA60-10uL", "TRICH", "TRICH 250  (10)", "Ultrio Elite", "WNV", "ZIKV", "ZIKV  (51)" };
                            string[] assaylist2 = { "AC2 100  (21)", "AC2 250  (1)", "AdV/hMPV/RV  (102)", "Babesia  (50)", "C. diff  (108)", "CT 100  (22)", "CT 250  (5)", "DENV  (36)", "dHBV  (97)", "dHCV  (98)", "dHIV  (99)", "DNA60  (1507)", "Flu A/B/RSV  (101)", "GBS  (106)", "GC 100  (23)", "GC 250  (6)", "GT HPV  (20)", "HEV  (39)", "HPV 100  (24)", "HPV 250  (14)", "HSV 1&2  (40)", "LDT RT TMA-1  (90)", "LDT RT TMA-2  (91)", "LDT RT TMA-3  (92)", "LDT TMA-1  (95)", "LDT TMA-2  (88)", "LDT TMA-3  (89)", "LDT1  (1001)", "M gen  (43)", "MRSA  (107)", "OADNA55  (1505)", "OADNA58  (1506)", "OADNA62  (1508)", "OADNA65  (1509)", "Paraflu  (103)", "Parvo/HAV  (29)", "PSA 100  (31)", "QCDNAIC  (1501)", "QCFluPPR  (1503)", "QCRNAIC  (1502)", "qHBV  (34)", "qHCV  (32)", "qHIV-1  (28)", "RNA60  (1510)", "RNA60-10uL  (1512)", "TRICH 100  (9)", "TRICH 250  (10)", "Ultrio Elite  (25)", "WNV  (4)", "ZIKV  (52)", "ZIKV  (51)" };
                            int result = f.ToText(pathToText);
                            if (result == 0)
                            {
                                var lineCount = File.ReadLines(pathToText).Count();
                                Debug.WriteLine(lineCount);
                                FileStream inFile = new FileStream(pathToText, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                                using (StreamReader sr = new StreamReader(inFile))
                                {
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        count++;
                                        foreach (string word in assaylist1)
                                        {
                                            if (line.Contains(word))
                                            {
                                                var indexOfAssay = Array.IndexOf(assaylistFromDB, word);
                                                var assayNameExtracted = assaylist2[indexOfAssay];
                                                assayNames.Add(assayNameExtracted);
                                                flag = 1;
                                                break;
                                            }
                                        }
                                        if (flag == 1)
                                        {
                                            break;
                                        }
                                    }
                                    Debug.WriteLine(count);
                                    if (count == lineCount)
                                    {
                                        var assayNameExtracted = "Choose AssayName";
                                        assayNames.Add(assayNameExtracted);
                                    }
                                }
                                if (assayNames[i] == "")
                                {
                                    assayNames.Add("Choose AssayName");

                                }
                            }
                            logData.Info("Returning the assay name");
                        }
                        catch (Exception e)
                        {
                            logData.Error(e.Message);
                        }
                    f.ClosePdf();
                    }
                    else
                    {
                        assayNames.Add("Choose AssayName");
                    }
                }
                return assayNames;
           // }
        }

        // PUT: api/ExtractAssayName/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ExtractAssayName/5
        public void Delete(int id)
        {
        }
    }
}
