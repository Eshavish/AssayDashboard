using Hologic.AssayDashboard.Database.Models;
using Hologic.AssayDashboard.Database.repositories;
using Hologic.AssayDashboard.Database.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hologic.AssayDashboard.WebAPI.Controllers
{
    public class AssaySSVQueryController : ApiController
    {

        private static readonly log4net.ILog logData = log4net.LogManager
      .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public class FileStructure
        {
            public string AssayName { get; set; }

            public ReportFile File { get; set; }
        }
        // GET: api/AssaySSVQuery
        [HttpGet, Route("api/AssaySSVQuery/{typeIdToSend}/{majorVersion}/{minorVersion}/{servicePack}/{buildNumber}")]
        public string Get(int typeIdToSend, int majorVersion, int minorVersion, int servicePack, int buildNumber)
        {
            logData.Info("Get files with report svv filter executing");
            var reportTypeId = new ExtractReportTypeId();
            var assayTypeRepo = new ExtractAssayType();
            var extractFileRepo = new ExtractFile();
            var extractReportSsv = new ExtractReportSSV();
            try
            {
                long Id = 0;
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

                //if only assay and  major version is given
                if (typeIdToSend != -1 && majorVersion != -1 && minorVersion == -1 && servicePack == -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileRepo.GetReportWithAssayMajor(Id, majorVersion);
                    return returnResponseFile(tempHolder);
                }

                //if only assay andminor version is given

                if (typeIdToSend != -1 && majorVersion == -1 && minorVersion != -1 && servicePack == -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileRepo.GetReportWithAssayMinor(Id, minorVersion);
                    return returnResponseFile(tempHolder);
                }

                //if only assay and service number version is given
                if (typeIdToSend != -1 && majorVersion == -1 && minorVersion == -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileRepo.GetReportWithAssayServicePack(Id, servicePack);
                    return returnResponseFile(tempHolder);
                }
                //if only assay and build number version is given
                if (typeIdToSend != -1 && majorVersion == -1 && minorVersion == -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileRepo.GetReportWithAssayBuildNumber(Id, buildNumber);
                    return returnResponseFile(tempHolder);
                }

                //if  assay, major and minor are given
                if (typeIdToSend != -1 && majorVersion != -1 && minorVersion != -1 && servicePack == -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileRepo.GetReportWithAssayMajorMinor(Id, majorVersion, minorVersion);
                    return returnResponseFile(tempHolder);
                }
               
               // if assay, major and service are given
               if (typeIdToSend != -1 && majorVersion != -1 && minorVersion == -1 && servicePack != -1 && buildNumber == -1)
               {
                   var tempHolder = extractFileRepo.GetReportWithAssayMajorService(Id, majorVersion, servicePack);
                   return returnResponseFile(tempHolder);
               }

               //if assay,  major and build are given
               if (typeIdToSend != -1 && majorVersion != -1 && minorVersion == -1 && servicePack == -1 && buildNumber != -1)
               {
                   var tempHolder = extractFileRepo.GetReportWithAssayMajorBuild(Id, majorVersion, buildNumber);
                   return returnResponseFile(tempHolder);
               }
               //if assay, major minor and service are given
               if (typeIdToSend != -1 && majorVersion != -1 && minorVersion != -1 && servicePack != -1 && buildNumber == -1)
               {
                   var tempHolder = extractFileRepo.GetReportWithAssayMajorMinorService(Id, majorVersion, minorVersion, servicePack);
                   return returnResponseFile(tempHolder);
               }

               //if assay, major minor and build are given
               if (typeIdToSend != -1 && majorVersion != -1 && minorVersion != -1 && servicePack == -1 && buildNumber != -1)
               {
                   var tempHolder = extractFileRepo.GetReportWithAssayMajorMinorBuild(Id, majorVersion, minorVersion, buildNumber);
                   return returnResponseFile(tempHolder);
               }
                
               // assay, major service build are given
               if (typeIdToSend != -1 && majorVersion != -1 && minorVersion == -1 && servicePack != -1 && buildNumber != -1)
               {
                   var tempHolder = extractFileRepo.GetReportWithAssayMajorServiceBuild(Id, majorVersion, servicePack, buildNumber);
                   return returnResponseFile(tempHolder);
               }

               // assay, minor and service are given
               if (typeIdToSend != -1 && majorVersion == -1 && minorVersion != -1 && servicePack != -1 && buildNumber == -1)
               {
                   var tempHolder = extractFileRepo.GetReportWithAssayMinorService(Id, minorVersion, servicePack);
                   return returnResponseFile(tempHolder);
               }
               // assay, minor and build are given 
               if (typeIdToSend != -1 && majorVersion == -1 && minorVersion != -1 && servicePack == -1 && buildNumber != -1)
               {
                   var tempHolder = extractFileRepo.GetReportWithAssayMinorBuild(Id, minorVersion, buildNumber);
                   return returnResponseFile(tempHolder);
               }
               // assay, minor service and build are given
               if (typeIdToSend != -1 && majorVersion == -1 && minorVersion != -1 && servicePack != -1 && buildNumber != -1)
               {
                   var tempHolder = extractFileRepo.GetReportWithAssayMinorServiceBuild(Id, minorVersion, servicePack, buildNumber);
                   return returnResponseFile(tempHolder);
               }
               // assay, service and build are given 
               if (typeIdToSend != -1 && majorVersion == -1 && minorVersion == -1 && servicePack != -1 && buildNumber != -1)
               {
                   var tempHolder = extractFileRepo.GetReportWithAssayServiceBuild(Id, servicePack, buildNumber);
                   return returnResponseFile(tempHolder);
               }


            }
            catch (Exception e)
            {


            }
            return null;
        }

      
        // POST: api/AssaySSVQuery
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AssaySSVQuery/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AssaySSVQuery/5
        public void Delete(int id)
        {
        }
    }
}
