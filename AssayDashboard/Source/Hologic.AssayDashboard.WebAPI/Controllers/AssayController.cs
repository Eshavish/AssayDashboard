using Hologic.AssayDashboard.Database.Models;
using Hologic.AssayDashboard.Database.Repositories;
using Hologic.AssayDashboard.WebApi.services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hologic.ReportRepo.WebApi.Controllers
    
{
    public class AssayController : ApiController
    {

        private static readonly log4net.ILog logData = log4net.LogManager
        .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<object> Get()
        {
            logData.Info("Get Assay Function is called");
            // getting back the assaytypes from the database table( AssayTypes)
            using (var repo = new AssayRepository())
            {
                try
                {
                    List<DisplayClass> objectAssay = new List<DisplayClass>();
                    var assaynamesobj = repo.GetAssay();
                    foreach (var assaytype in assaynamesobj)
                     {
                        DisplayClass ds = new DisplayClass();
                        ds.Id = assaytype.ID;
                        ds.TypeId = assaytype.TypeId;
                        ds.AssayName = assaytype.AssayName;
                        objectAssay.Add(ds);
                    }
                    
                    logData.Info("Get Assay Function is finished");
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
    }
}
