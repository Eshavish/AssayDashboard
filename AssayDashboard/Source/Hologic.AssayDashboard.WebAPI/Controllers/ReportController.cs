using Hologic.AssayDashboard.Database.Models;
using Hologic.AssayDashboard.Database.repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hologic.AssayDashboard.WebApi.Controllers
{
    public class ReportController : ApiController
    {
        private static readonly log4net.ILog logData = log4net.LogManager
    .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<string> Get()
        {
            // getting back the reporttypes from the database table( ReportTypes)
            using (var repo = new ReportRepoClass())
            {
                logData.Info("Getting Report Types from db");
                try
                {
                    List<string> reportTypeList = new List<string>();
                    var results = repo.get();
                    foreach (var reporttype in results)
                    {
                        reportTypeList.Add(reporttype.ReportName);
                    }
                    logData.Info("Getting Report Types Finished");
                    return reportTypeList;            
                }
                catch (SqlException odbcEx)
                {
                    Debug.WriteLine(odbcEx.Message);
                    logData.Error(odbcEx.Message);
                    return null;
                }
            }
        }
    }
}
