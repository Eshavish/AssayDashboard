using Hologic.AssayDashboard.Database.Models;
using Hologic.AssayDashboard.Database.repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hologic.AssayDashboard.WebAPI.Controllers
{
    public class ReportAssaySSVQueryController : ApiController
    {
        private static readonly log4net.ILog logData = log4net.LogManager
     .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public class FileStructure
        {
            public string AssayName { get; set; }

            public ReportFile File { get; set; }
        }
        // GET: api/ReportAssaySSVQuery
        [HttpGet, Route("api/ReportAssaySSVQuery/{typeIdToSend}/{reportType}/{majorVersion}/{minorVersion}/{servicePack}/{buildNumber}")]
        public string Get(int typeIdToSend, string reportType, int majorVersion, int minorVersion, int servicePack, int buildNumber)
        {
            var assayTypeRepo = new ExtractAssayType();
            var extractFileRepo = new ExtractFile();
            var reportTypeId = new ExtractReportTypeId();
            var extractFileWithReportAssaySsv = new ExtractAssayType();

            try
            {
                long Id = 0;
                long reportId = 0;
                logData.Info("Get files with report assay ssv filter executing");
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
                if (typeIdToSend != -1)
                {
                    Id = assayTypeRepo.GetAssayID(typeIdToSend);

                }
                //if report type is not null
                if (reportType != "null")
                {
                    reportId = reportTypeId.GetReportTypeId(reportType);

                }
                //if only report, assay and major version is given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion != -1 && minorVersion == -1 && servicePack == -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayMajorVersion(Id, reportId, majorVersion);
                    return returnResponseFile(tempHolder);
                }

                //if only report assay and build are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion == -1 && minorVersion == -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayBuildNumber(Id, reportId, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //if only report assay and service are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion == -1 && minorVersion == -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayServicePack(Id, reportId, servicePack);
                    return returnResponseFile(tempHolder);
                }
                //if only report assay and minor are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion == -1 && minorVersion != -1 && servicePack == -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayMinor(Id, reportId, minorVersion);
                    return returnResponseFile(tempHolder);
                }
                //if only report assay minor major are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion != -1 && minorVersion != -1 && servicePack == -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayMinorMajor(Id, reportId, majorVersion, minorVersion);
                    return returnResponseFile(tempHolder);
                }
                //if only report assay service major are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion != -1 && minorVersion == -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayMajorServicePack(Id, reportId, majorVersion, servicePack);
                    return returnResponseFile(tempHolder);
                }

                //if only report assay build major are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion != -1 && minorVersion == -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayMajorBuildNumber(Id, reportId, majorVersion, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //if only report assay service minor are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion == -1 && minorVersion != -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayMinorService(Id, reportId, minorVersion, servicePack);
                    return returnResponseFile(tempHolder);
                }
                //if only report assay build minor are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion == -1 && minorVersion != -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayMinorBuild(Id, reportId, minorVersion, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //if only report assay build service are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion == -1 && minorVersion == -1 && servicePack != -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayBuildService(Id, reportId, servicePack, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //if only report assay major minor service are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion != -1 && minorVersion != -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayMajorMinorService(Id, reportId, majorVersion, minorVersion,servicePack);
                    return returnResponseFile(tempHolder);
                }
                //if only report assay major minor build are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion != -1 && minorVersion != -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayMajorMinorBuild(Id, reportId, majorVersion, minorVersion, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //if only report assay  minor service build are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion == -1 && minorVersion != -1 && servicePack != -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayMinorServiceBuild(Id, reportId, minorVersion, servicePack, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //if only report assay  major service build are given
                if (typeIdToSend != -1 && reportType != "null" && majorVersion != -1 && minorVersion == -1 && servicePack != -1 && buildNumber != -1)
                {
                    var tempHolder = extractFileWithReportAssaySsv.GetReportWithReportAssayMajorServiceBuild(Id, reportId, majorVersion, servicePack, buildNumber);
                    return returnResponseFile(tempHolder);
                }

            }
            catch (Exception e)
            {
                logData.Error("Get files failed");
                Console.WriteLine(e.Message);
            }

            return null;
        }

      
        // POST: api/ReportAssaySSVQuery
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ReportAssaySSVQuery/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ReportAssaySSVQuery/5
        public void Delete(int id)
        {
        }
    }
}
