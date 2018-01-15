using Hologic.AssayDashboard.Database.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using Hologic.AssayDashboard.Database.repositories;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics;
using System.Collections;

namespace Hologic.AssayDashboard.WebApi.Controllers
{
   
    public class ValuesController : ApiController
    {
        private static readonly log4net.ILog logData = log4net.LogManager
        .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public class FileStructure
        {
            public string AssayName { get; set; }

            public ReportFile File { get; set; }
        }
        // GET api/Values

        [HttpGet, Route("api/Values/{typeIdToSend}/{reportType}/{majorVersion}/{minorVersion}/{servicePack}/{buildNumber}")]
        public string Get(int typeIdToSend, string reportType, int majorVersion, int minorVersion, int servicePack, int buildNumber)
        {

            logData.Info("Get files executing");
     
            var extractFileParamsRepo = new ExtractReportFileWithParams();
            var reportTypeId = new ExtractReportTypeId();
            var assayTypeRepo = new ExtractAssayType();
            var extractFileRepo = new ExtractFile();
  
            try
            {
                    long Id=0;
                    long reportId=0;
                   // var files = new List<FileStructure>();
                string returnResponseFile(IEnumerable<ReportFile> temp)
                {
                    var files = new List<FileStructure>();
                    foreach (var t in temp)
                    {
                        files.Add(new FileStructure
                        {
                            AssayName = t.FileName,
                            File = t
                        });
                    }
                    return JsonConvert.SerializeObject(files);
                }
                //if assay name is not null
                if (typeIdToSend != -1)
                    {
                        Id = assayTypeRepo.GetAssayID(typeIdToSend);

                    }
                    //if report type is not null
                    if (reportType!="null")
                    {
                        reportId = reportTypeId.GetReportTypeId(reportType);

                    }

                   //if only assay name is mentioned
                    if (typeIdToSend != -1 &&  reportType=="null" && majorVersion==-1 && minorVersion==-1 && servicePack==-1 && buildNumber==-1)
                    {
                       var tempHolder = extractFileRepo.GetReportWithId(Id);
                        return returnResponseFile(tempHolder);                 
                    }
                //if only report type is mentioned
                if (typeIdToSend == -1 && reportType != "null" && majorVersion==-1 && minorVersion==-1 && servicePack==-1 && buildNumber==-1)
                    {
                        var tempHolder = extractFileRepo.GetReportWithReportId(reportId);
                    return  returnResponseFile(tempHolder);
                    }
                    //if only the system software version all fields are not null
                    if (typeIdToSend == -1 && reportType=="null" && majorVersion != -1 && minorVersion != -1 && servicePack!=-1 && buildNumber!=-1)
                     {//GetReportWithSSV
                    var tempHolder = extractFileRepo.GetReportWithSSV(majorVersion, minorVersion, servicePack, buildNumber);
                    return  returnResponseFile(tempHolder);
                    }
         
                // if assay name and report type is not null
                if (typeIdToSend != -1 && reportType!="null" && majorVersion==-1 && minorVersion==-1 &&servicePack==-1 && buildNumber==-1)
                    {
                        var tempHolder = extractFileParamsRepo.GetReportWithAssayNameReportType(Id,reportId);
                    return  returnResponseFile(tempHolder);
                    }

                    //if assay name and system software version all fields is not null 
                    if (typeIdToSend != -1 && reportType=="null" && majorVersion!=-1 && minorVersion!=-1 && servicePack!=-1 && buildNumber!=-1)
                    {
                        var tempHolder = extractFileParamsRepo.GetReportWithAssayNameSSV(Id, majorVersion,minorVersion,servicePack,buildNumber);
                    return returnResponseFile(tempHolder);
                    }
                    //if report type and system software version is not null
                    if (typeIdToSend == -1 && reportType != "null" && majorVersion != -1 && minorVersion != -1 && servicePack != -1 && buildNumber != -1)
                    {
                        var tempHolder = extractFileParamsRepo.GetReportWithReportTypeSSV(reportId, majorVersion, minorVersion, servicePack, buildNumber);
                    return  returnResponseFile(tempHolder);
                    }

                    //if only major version is given
                    if(typeIdToSend == -1 && reportType == "null" && majorVersion != -1 && minorVersion == -1 && servicePack == -1 && buildNumber == -1)
                    {
                    var tempHolder = extractFileParamsRepo.GetReportWithMajorVersion(majorVersion);
                    return returnResponseFile(tempHolder);
                    }
                //if only minor version is given

                if (typeIdToSend == -1 && reportType == "null" && majorVersion == -1 && minorVersion != -1 && servicePack == -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithMinorVersion(minorVersion);
                    return returnResponseFile(tempHolder);
                }
                //if only service number version is given
                if (typeIdToSend == -1 && reportType == "null" && majorVersion == -1 && minorVersion == -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithServicePack(servicePack);
                    return returnResponseFile(tempHolder);
                }
                //if only build number version is given
                if (typeIdToSend == -1 && reportType == "null" && majorVersion == -1 && minorVersion == -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithBuildNumber(buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //if major and minor are given
                if (typeIdToSend == -1 && reportType == "null" && majorVersion != -1 && minorVersion != -1 && servicePack == -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithMajorMinor(majorVersion, minorVersion);
                    return returnResponseFile(tempHolder);
                }
                // if major and service are given
                if (typeIdToSend == -1 && reportType == "null" && majorVersion != -1 && minorVersion == -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithMajorService(majorVersion, servicePack);
                    return returnResponseFile(tempHolder);
                }
                //if major and build are given
                if (typeIdToSend == -1 && reportType == "null" && majorVersion != -1 && minorVersion == -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithMajorBuild(majorVersion, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //if major minor and service are given
                if (typeIdToSend == -1 && reportType == "null" && majorVersion != -1 && minorVersion != -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithMajorMinorService(majorVersion, minorVersion, servicePack);
                    return returnResponseFile(tempHolder);
                }
                //if major minor and build are given
                if (typeIdToSend == -1 && reportType == "null" && majorVersion != -1 && minorVersion != -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithMajorMinorBuild(majorVersion, minorVersion, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //major service build are given
                if (typeIdToSend == -1 && reportType == "null" && majorVersion != -1 && minorVersion == -1 && servicePack != -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithMajorServiceBuild(majorVersion, servicePack, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //minor and service are given
                if (typeIdToSend == -1 && reportType == "null" && majorVersion == -1 && minorVersion != -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithMinorService(minorVersion, servicePack);
                    return returnResponseFile(tempHolder);
                }
                //minor and build are given 
                if (typeIdToSend == -1 && reportType == "null" && majorVersion == -1 && minorVersion != -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithMinorBuild(minorVersion, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //minor service and build are given
                if (typeIdToSend == -1 && reportType == "null" && majorVersion == -1 && minorVersion != -1 && servicePack != -1 && buildNumber != -1)
                {//GetReportWithMinorServiceBuild
                   // extractFileParamsRepo
                    var tempHolder = extractFileParamsRepo.GetReportWithMinorServiceBuild(minorVersion, servicePack, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //service and build are given 
                if (typeIdToSend == -1 && reportType == "null" && majorVersion == -1 && minorVersion == -1 && servicePack != -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileParamsRepo.GetReportWithServiceBuild(servicePack, buildNumber);
                    return returnResponseFile(tempHolder);
                }
              
                //if assay name, report type and system  software is not null
                if (typeIdToSend != -1 && reportType != "null" && majorVersion != -1 && minorVersion != -1 && servicePack != -1 && buildNumber != -1)
                    {
                        var tempHolder = extractFileParamsRepo.GetReportWithAssayNameReportTypeSSV(Id,reportId, majorVersion, minorVersion, servicePack, buildNumber);
                    return returnResponseFile(tempHolder);
                    }
                }
            catch (FileNotFoundException e)
            {
                logData.Error("Get files failed file exception");
                Console.WriteLine("File" + e.FileName + "not found");
            }

            catch (Exception ex)
            {
                logData.Error("Get files failed");
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        // POST api/values
       
        public void Post([FromBody] string value)
        {
            
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
