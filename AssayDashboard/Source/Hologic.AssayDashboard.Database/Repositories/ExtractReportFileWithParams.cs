using Hologic.AssayDashboard.Database.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Database.repositories
{
    public class ExtractReportFileWithParams : IDisposable
    {
        private readonly AssayDashboardContext _dataContext;
        public ExtractReportFileWithParams()
        {
            _dataContext = new AssayDashboardContext();
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }
        public IEnumerable<ReportFile> GetReportWithServicePack(int servicePack)
        {
            // Debug.WriteLine("Test " +  _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ServicePackNumber == servicePack).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ServicePackNumber == servicePack).ToList();
        }
        public IEnumerable<ReportFile> GetReportWithBuildNumber(int buildNumber)
        {
            // Debug.WriteLine("Test " +  _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ServicePackNumber == servicePack).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.BuildNumber == buildNumber).ToList();
        }
        public IEnumerable<ReportFile> GetReportWithMinorVersion(int minorVersion)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion).ToList();
        }
        
            public IEnumerable<ReportFile> GetReportWithMajorService(int majorVersion, int servicePack)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorVersion && reportFileTable.ServicePackNumber== servicePack).ToList();
        }
        public IEnumerable<ReportFile> GetReportWithMajorMinorBuild(int majorVersion, int minorVersion, int buildNumber)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithMinorService(int minorVersion, int servicePack)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber== servicePack).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithMinorBuild(int minorVersion, int buildNumber)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithMinorServiceBuild(int minorVersion, int servicePack, int buildNumber)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber== servicePack  && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithServiceBuild(int servicePack, int buildNumber)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }


        public IEnumerable<ReportFile> GetReportWithMajorServiceBuild(int majorVersion, int servicePack, int buildNumber)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }

        public IEnumerable<ReportFile> GetReportWithMajorBuild(int majorVersion, int buildNumber)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        public IEnumerable<ReportFile> GetReportWithMajorMinorService(int majorVersion, int minorVersion,int servicePack)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MinorVersion == minorVersion).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber== servicePack).ToList();
        }
        public IEnumerable<ReportFile> GetReportWithMajorMinor(int majorVersion, int minorVersion)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorwildentry && reportFileTable.MinorVersion== minorwildentry).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion== minorVersion).ToList();
        }
        public IEnumerable<ReportFile> GetReportWithMajorVersion(int majorVersion)
        {
            // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorVersion).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.MajorVersion == majorVersion).ToList();
        }
        public IEnumerable<ReportFile> GetReportWithAssayNameReportType(long assayNameId, long reportTypeId)
        {
           // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == assayNameId && reportFileTable.ReportTypeId == reportTypeId).ToList().ElementAt<ReportFile>(0).FileName);
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == assayNameId && reportFileTable.ReportTypeId == reportTypeId).ToList();
        }
        public IEnumerable<ReportFile> GetReportWithAssayNameSSV(long assayNameId, int majorVersion, int minorVersion, int servicePack, int buildNumber)
        {
           // Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == assayNameId && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion== minorVersion && reportFileTable.ServicePackNumber== servicePack && reportFileTable.BuildNumber== buildNumber).ToList().ElementAt<ReportFile>(0).FileName);
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == assayNameId && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        public IEnumerable<ReportFile> GetReportWithReportTypeSSV(long reportTypeId, int majorVersion, int minorVersion, int servicePack, int buildNumber)
        {
          //  Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList().ElementAt<ReportFile>(0).FileName);
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        public IEnumerable<ReportFile> GetReportWithAssayNameReportTypeSSV(long id,long reportTypeId, int majorVersion, int minorVersion, int servicePack, int buildNumber)
        {
          //  Debug.WriteLine("Test " + _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.AssayTypeId==id && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList().ElementAt<ReportFile>(0).FileName);
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }
    }
}
