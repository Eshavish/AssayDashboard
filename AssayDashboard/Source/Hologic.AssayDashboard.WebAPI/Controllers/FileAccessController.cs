// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------

using System.Collections.Generic;
using System.Web.Http;
using Hologic.AssayDashboard.Database.Models;
using Hologic.AssayDashboard.Database.Repositories;
using Hologic.AssayDashboard.Domain.Services;
using System.Web;
using System.IO;
using System;
using System.Text;
using System.Collections;
using System.Net.Http;
using System.Net;
using System.Web.Http.Results;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using log4net;
using static Hologic.AssayDashboard.Domain.Services.CurveDataPointService;

namespace Hologic.AssayDashboard.WebAPI.Controllers
{
    public class FileAccessController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager
        .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public class Pair
        {
            public string AssayName { get; set; }

            public CurveFile curveFile { get; set; }

            public DataSet data { get; set; }

            public string FileType { get; set; }
        }
        
        public class FileAndAssay
        {
            public string FileName { get; set; }
            public string Assay { get; set; }
        }

        public class AssayCount
        {
            public AssayType assay { get; set; }
            public long fileCount { get; set; }
        }

        //Getting AssayType table
        [HttpGet, Route("api/FileAccess/getAssay")]
        public string Get()
        {
            log.Info("getAssay method is called");
            IEnumerable<AssayType> container = null;
            var toReturn = new List<AssayCount>();

            var repo = new AssayRepository();
            var curveRepo = new CurveFilesRepository();
            container = repo.GetAssay();
            foreach (var item in container)
            {
                toReturn.Add(new AssayCount
                {
                    assay = item,
                    fileCount = curveRepo.GetCountOfCurve(item.TypeId)
                });
            }
            log.Info("getAssay method is finished");
            return JsonConvert.SerializeObject(toReturn);
        }

        [HttpGet, Route("api/FileAccess/getAssayInDB")]
        public string GetAssayInDB()
        {
            IEnumerable<long> container = null;
            List<AssayType> assayContainer = new List<AssayType>();

            List<AssayType> toReturn = new List<AssayType>();

            using (var repo = new CurveFilesRepository())
            {
                container = repo.GetUniqueTypeID();
            }
            var assayRepo = new AssayRepository();
                foreach (var item in container)
                {
                    assayContainer.Add(assayRepo.GetAssayUsingTypeID(item));
                }
            
            return JsonConvert.SerializeObject(assayContainer);
        }

        [HttpGet, Route("api/FileAccess/getVersion")]
        public Object GetAssayVersion()
        { 
            using (var repo = new CurveFilesRepository())
            {
                return JsonConvert.SerializeObject(repo.GetVersion());
            }
        }



        //api/FileAccess
        [HttpGet, Route("api/FileAccess/{IsGolden}/{Tag}/{AssayVersion}/{SoftwareVersion}/{AssayID}")]
        public string Get(Boolean IsGolden, string Tag, string AssayVersion, string SoftwareVersion, int AssayID)
        {
            long AssayDBID;
            string AssayName = "";
            //Assay name is specified
            if (AssayID != -1)
            {
                using (var repo = new AssayRepository())
                {
                    AssayDBID = repo.GetTypeIdWithID(AssayID);
                    AssayName = repo.GetAssayName(AssayID);
                }
            }
            else
            {
                AssayDBID = -1;
            }
            var files = new List<Pair>();
            if (Tag == "undefined")
            {
                Tag = "";
            }
            if (AssayVersion == "undefined")
            {
                AssayVersion = "";
            }
            if (SoftwareVersion == "undefined")
            {
                SoftwareVersion = "";
            }

            using (var repo = new CurveFilesRepository())
            {
                var tempContainer = repo.GetSpecifiedFile(IsGolden, Tag, AssayVersion, SoftwareVersion, AssayDBID);
                if (tempContainer.Count<CurveFile>() == 0)
                {
                    return JsonConvert.SerializeObject("No Matching Curve File Found");
                }
                else
                {
                    var service = new CurveDataPointService();
                    var assayRepo = new AssayRepository();
               
                 
                    foreach (var temp in tempContainer)
                    {
                        var container = service.getCurveData(temp.Data, temp.FullFileName);

                        if (AssayDBID == -1)
                        {
                            AssayName = assayRepo.GetAssayNameUsingTypeID(temp.AssayDBID);
                        }

                        files.Add(new Pair
                        {
                            AssayName = AssayName,
                            curveFile = temp,
                            data = container.dataSet,
                            FileType = container.FileType
                        });
                    }
                }
            } 
            return JsonConvert.SerializeObject(files);
        }

        [HttpGet, Route("api/FileAccess/getKnown")]
        public string getKnown()
        {
            var toReturn = "";

            using (var repo = new CurveFilesRepository())
            {
                toReturn = repo.GetNumberOfCurve().ToString();
                if (toReturn == "0")
                {
                    return "";
                }
                toReturn += ',';
                toReturn += repo.GetNumberOfKnown().ToString();
                return toReturn;
            }
        }
        //api/FileAccess/SetAssay
        [HttpPost, Route("api/FileAccess/SetAssay")]
        public string lookUpAssayInFile()
        {
            FileValidationService fileService = new FileValidationService();
            var results = new List<FileAndAssay>();

            for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                HttpPostedFile file = HttpContext.Current.Request.Files.Get(i);

                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    var fileBlob = binaryReader.ReadBytes(file.ContentLength);
                    var fileName = HttpContext.Current.Request[$"file-metadata-{i}"];

                    var delim = new[] { '"' };
                    fileName = Regex.Unescape(fileName);
                    fileName = fileName.TrimEnd(delim).TrimStart(delim);

                    var AssayName = fileService.getAssayFromFile(fileBlob, fileName);
                    
                    results.Add(new FileAndAssay
                    {
                        FileName = fileName,
                        Assay = AssayName
                    });
                }
            }
            return JsonConvert.SerializeObject(results);
        }

        [HttpPost]
      public string Post()
      {
         FileUploadService service = new FileUploadService();
         FileValidationService fileService = new FileValidationService();
         CurveFile[] toAdd = new CurveFile[HttpContext.Current.Request.Files.Count];
         var j = 0;
         ArrayList InvalidFile = new ArrayList();

          for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
          {
              HttpPostedFile file = HttpContext.Current.Request.Files.Get(i);

              using (var binaryReader = new BinaryReader(file.InputStream))
              {
                  var fileBlob = binaryReader.ReadBytes(file.ContentLength);
                  var fileJsonMeta = HttpContext.Current.Request[$"file-metadata-{i}"];
                  var AssayName = HttpContext.Current.Request[$"file-assayname-{i}"];
                

                  //sets up curve object attributes
                  var curveFile = service.Transform(fileJsonMeta, AssayName);
                  curveFile.Data = fileBlob;
                  curveFile.SoftwareVersion = service.getVersionNum("SoftwareVersion", curveFile.Data, curveFile.FullFileName);
                  curveFile.AssayVersion = service.getVersionNum("AssayVersion", curveFile.Data, curveFile.FullFileName);


                  var result = fileService.ValidateFile(curveFile.Data, curveFile.FullFileName, AssayName);

                    //file validation
                    if (result.Indicator){
                        toAdd[j] = curveFile;
                        j++;
                    }
                    else
                    {
                        InvalidFile.Add(curveFile.FullFileName + result.message);
                    }
              }
          }
            using (var repo = new CurveFilesRepository())
            {
                repo.AddCurveFile(toAdd);
            }
            //Httpresponse message with invalid files
            if (InvalidFile.Count != 0)
            {
                return JsonConvert.SerializeObject(InvalidFile);
            }
           return JsonConvert.SerializeObject("Successful");
        }
    }
}
