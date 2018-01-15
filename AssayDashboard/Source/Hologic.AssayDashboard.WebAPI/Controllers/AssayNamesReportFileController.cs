using Hologic.AssayDashboard.Database.Repositories;
using Hologic.AssayDashboard.WebApi.services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hologic.AssayDashboard.WebAPI.Controllers
{
    public class AssayNamesReportFileController : ApiController
    {
        private static readonly log4net.ILog logData = log4net.LogManager
       .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: api/AssayNamesReportFile
        public IEnumerable<object> Get()
        {
            logData.Info("Get Assay Name from Report File Function is called");
            // getting assay id from the database table( ReportFile)
            using (var repo = new AssayRepository())
            {
                try
                {
                    List<DisplayClass> objectAssay = new List<DisplayClass>();
                    List<long> ids = new List<long>();
                    List<long> typeId = new List<long>();
                    List<string> assayNames = new List<string>();
                    var assayobject = repo.GetAssayNameFromReportFile();
                    foreach (var assayName in assayobject)
                    {
                        DisplayClass ds = new DisplayClass();
                        ids.Add(assayName.AssayTypeId);
                        ds.Id=assayName.AssayTypeId;
                    }                
 
                    //Using the assayId getting back the assay names
                    foreach(var assayid in ids)
                    {
                        DisplayClass ds = new DisplayClass();
                        ds.AssayName=repo.GetAssayName(assayid);
                        ds.TypeId=repo.GetTypeIdWithID(assayid);
                        objectAssay.Add(ds);
                    }
                    return objectAssay;
                }

                catch (SqlException odbcEx)
                {
                    Console.WriteLine(odbcEx.Message);
                    logData.Error(odbcEx.Message);
                    return null;
                }
            }
        }

        // GET: api/AssayNamesReportFile/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AssayNamesReportFile
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AssayNamesReportFile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AssayNamesReportFile/5
        public void Delete(int id)
        {
        }
    }
}
