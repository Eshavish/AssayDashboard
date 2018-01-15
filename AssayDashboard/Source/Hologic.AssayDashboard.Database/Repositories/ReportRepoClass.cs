#region Header
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
#endregion Header

using System;
using Hologic.AssayDashboard.Database.Models;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Hologic.AssayDashboard.Database.repositories
{

    public class ReportRepoClass : IDisposable
    {
        private readonly AssayDashboardContext _dataContext;
        public ReportRepoClass()
        {
            _dataContext = new AssayDashboardContext();
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }
        /*public IEnumerable<ReportFile> checkIfFileExists(string filedetails, string hashValue)
        {
            var delim = new[] { '"' };
            filedetails = Regex.Unescape(filedetails);
            filedetails = filedetails.TrimEnd(delim).TrimStart(delim);
            dynamic data = JObject.Parse(filedetails);
            //get all records with hash value and with reort type 
             return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == data.ReportTypeID).ToList();
     
        }*/
        public void sendToReportFile(byte[] serializedFile, string filedetails,string hashvalue)
        {
            // SAVING FILES TO THE DATABASE
            var delim = new[] { '"' };
            filedetails = Regex.Unescape(filedetails);
            filedetails = filedetails.TrimEnd(delim).TrimStart(delim);
            dynamic data = JObject.Parse(filedetails);
             var reportTypeObject = new ReportFile { FileName = data.FileName, AssayTypeId = data.AssayNameID, ReportTypeId = data.ReportTypeID, MajorVersion = data.MajorVersion, MinorVersion = data.MinorVersion, ServicePackNumber = data.ServicePackNumber, BuildNumber = data.BuildNumber, FileContent = serializedFile, LastModifiedOn = data.LastModifiedOn, LastModifiedBy = data.LastModifiedBy,HashValue=hashvalue};
            _dataContext.ReportFile.Add(reportTypeObject);
            _dataContext.SaveChanges();
        }
        
        public IEnumerable<ReportType> get()
        {
            return _dataContext.ReportType.ToList();
        }
    }
}
