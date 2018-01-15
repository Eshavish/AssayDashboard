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
    public class ReportSSVQueryController : ApiController
    {
        private static readonly log4net.ILog logData = log4net.LogManager
       .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public class FileStructure
        {
            public string AssayName { get; set; }

            public ReportFile File { get; set; }
        }
        // GET: api/ReportSSVQuery
        [HttpGet, Route("api/ReportSSVQuery/{reportType}/{majorVersion}/{minorVersion}/{servicePack}/{buildNumber}")]
        public string Get(string reportType, int majorVersion, int minorVersion, int servicePack, int buildNumber)
        {

            logData.Info("Get files with report svv filter executing");
            var reportTypeId = new ExtractReportTypeId();
            var extractReportSsv = new ExtractReportSSV();

            try
            {
                long reportId = 0;             
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

                if (reportType != "null")
                {
                    reportId = reportTypeId.GetReportTypeId(reportType);

                }
                //if only report and  major version is given
                if (reportType != "null" && majorVersion != -1 && minorVersion == -1 && servicePack == -1 && buildNumber == -1)
                {
                    var tempHolder = extractReportSsv.GetReportWithReportMajor(reportId, majorVersion);
                    return returnResponseFile(tempHolder);
                }
               
                //if only report and minor version is given

                if (reportType != "null" && majorVersion == -1 && minorVersion != -1 && servicePack == -1 && buildNumber == -1)
                {
                    var tempHolder = extractReportSsv.GetReportWithReportMinor(reportId,minorVersion);
                    return returnResponseFile(tempHolder);
                }
                
               //if only report and service number version is given
               if (reportType != "null" && majorVersion == -1 && minorVersion == -1 && servicePack != -1 && buildNumber == -1)
               {
                   var tempHolder = extractReportSsv.GetReportWithReportServicePack(reportId,servicePack);
                   return returnResponseFile(tempHolder);
               }
                //if only report and build number version is given
                if ( reportType != "null" && majorVersion == -1 && minorVersion == -1 && servicePack == -1 && buildNumber != -1)
               {
                   var tempHolder = extractReportSsv.GetReportWithReportBuildNumber(reportId,buildNumber);
                   return returnResponseFile(tempHolder);
               }

                //if  report, major and minor are given
                if (reportType != "null" && majorVersion != -1 && minorVersion != -1 && servicePack == -1 && buildNumber == -1)
                {
                    var tempHolder = extractReportSsv.GetReportWithReportMajorMinor(reportId,majorVersion, minorVersion);
                    return returnResponseFile(tempHolder);
                }
                // if report, major and service are given
                if (reportType != "null" && majorVersion != -1 && minorVersion == -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractReportSsv.GetReportWithReportMajorService(reportId,majorVersion, servicePack);
                    return returnResponseFile(tempHolder);
                }

                //if report,  major and build are given
                if (reportType != "null" && majorVersion != -1 && minorVersion == -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractReportSsv.GetReportWithReportMajorBuild(reportId, majorVersion, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                //if report, major minor and service are given
                if (reportType != "null" && majorVersion != -1 && minorVersion != -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractReportSsv.GetReportWithReportMajorMinorService(reportId, majorVersion, minorVersion, servicePack);
                    return returnResponseFile(tempHolder);
                }

                //if report, major minor and build are given
                if (reportType != "null" && majorVersion != -1 && minorVersion != -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractReportSsv.GetReportWithReportMajorMinorBuild(reportId, majorVersion, minorVersion, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                // report, major service build are given
                if (reportType != "null" && majorVersion != -1 && minorVersion == -1 && servicePack != -1 && buildNumber != -1)
                {
                    var tempHolder = extractReportSsv.GetReportWithReportMajorServiceBuild(reportId, majorVersion, servicePack, buildNumber);
                    return returnResponseFile(tempHolder);
                }

                // report, minor and service are given
                if (reportType != "null" && majorVersion == -1 && minorVersion != -1 && servicePack != -1 && buildNumber == -1)
                {
                    var tempHolder = extractReportSsv.GetReportWithReportMinorService(reportId, minorVersion, servicePack);
                    return returnResponseFile(tempHolder);
                }
                // report, minor and build are given 
                if (reportType != "null" && majorVersion == -1 && minorVersion != -1 && servicePack == -1 && buildNumber != -1)
                {
                    var tempHolder = extractReportSsv.GetReportWithReportMinorBuild(reportId,minorVersion, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                // report, minor service and build are given
                if (reportType != "null" && majorVersion == -1 && minorVersion != -1 && servicePack != -1 && buildNumber != -1)
                {//GetReportWithMinorServiceBuild
                 // extractFileParamsRepo
                    var tempHolder = extractReportSsv.GetReportWithReportMinorServiceBuild(reportId,minorVersion, servicePack, buildNumber);
                    return returnResponseFile(tempHolder);
                }
                // report, service and build are given 
                if (reportType != "null" && majorVersion == -1 && minorVersion == -1 && servicePack != -1 && buildNumber != -1)
                {
                    var tempHolder = extractReportSsv.GetReportWithReportServiceBuild(reportId, servicePack, buildNumber);
                    return returnResponseFile(tempHolder);
                }
               
            }
            catch (Exception e)
            {


            }
            return null;
        }



            // POST: api/ReportSSVQuery
            public void Post([FromBody]string value)
        {
        }

        // PUT: api/ReportSSVQuery/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ReportSSVQuery/5
        public void Delete(int id)
        {
        }
    }
}
