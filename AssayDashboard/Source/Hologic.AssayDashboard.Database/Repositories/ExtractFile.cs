using Hologic.AssayDashboard.Database.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hologic.AssayDashboard.Database.repositories
{
    public class ExtractFile : IDisposable
    {
        private readonly AssayDashboardContext _dataContext;
        public ExtractFile()
        {
            _dataContext = new AssayDashboardContext();
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }
        public IEnumerable<ReportFile> GetReportWithId(long id)
        {
            Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id).ToList().ElementAt<ReportFile>(0).FileContent);
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id).ToList();
        }

        public IEnumerable<ReportFile> GetReportWithReportId(long reportTypeId)
        {
            Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId).ToList().ElementAt<ReportFile>(0).FileContent);
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId).ToList();
        }
       
        public IEnumerable<ReportFile> GetReportWithSSV(int majorVersion, int minorVersion, int servicePack, int buildNumber)
        {
            Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList().ElementAt<ReportFile>(0).FileName);
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }

        /*(  public IEnumerable<ReportFile> GetReportData(int majorVersion=-1, int minorVersion=-1, int servicePack=-1, int buildNumber=-1, long reportId=-1)
          {
             // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList().ElementAt<ReportFile>(0).FileName);
              return _dataContext.ReportFile.Where(reportFileTable => (majorVersion != -1 && reportFileTable.MajorVersion == majorVersion ) && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
          }
          */
         public IEnumerable<ReportFile> GetReportWithAssayMajor(long id, int majorVersion)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.MajorVersion == majorVersion).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithAssayMinor(long id, int minorVersion)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.MinorVersion == minorVersion).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithAssayServicePack(long id, int servicePack)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ServicePackNumber == servicePack).ToList();
        }      
            public IEnumerable<ReportFile> GetReportWithAssayBuildNumber(long id, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithAssayMajorMinor(long id, int majorVersion, int minorVersion)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion== minorVersion).ToList();
        }
        
         public IEnumerable<ReportFile> GetReportWithAssayMajorService(long id, int majorVersion, int servicePack)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.MajorVersion == majorVersion && reportFileTable.ServicePackNumber == servicePack).ToList();
        }
        
     public IEnumerable<ReportFile> GetReportWithAssayMajorBuild(long id, int majorVersion, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.MajorVersion == majorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
       public IEnumerable<ReportFile> GetReportWithAssayMajorMinorService(long id, int majorVersion, int minorVersion, int servicePack)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber== servicePack).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithAssayMajorMinorBuild(long id, int majorVersion, int minorVersion, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithAssayMajorServiceBuild(long id, int majorVersion, int servicePack, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.MajorVersion == majorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithAssayMinorService(long id, int minorVersion, int servicePack)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id  && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack).ToList();
        }
        
     public IEnumerable<ReportFile> GetReportWithAssayMinorBuild(long id, int minorVersion, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.MinorVersion == minorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithAssayMinorServiceBuild(long id, int minorVersion, int servicePack, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.MinorVersion == minorVersion &&   reportFileTable.ServicePackNumber== servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithAssayServiceBuild(long id,int servicePack, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }
    }
}
