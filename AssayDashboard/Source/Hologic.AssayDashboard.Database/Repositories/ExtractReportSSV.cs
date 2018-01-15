using Hologic.AssayDashboard.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Database.Repositories
{
   public class ExtractReportSSV:IDisposable
    {
        private readonly AssayDashboardContext _dataContext;
        public ExtractReportSSV()
        {
            _dataContext = new AssayDashboardContext();
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }
         public IEnumerable<ReportFile> GetReportWithReportMajor(long reportTypeId, int majorVersion)
        {
            // Debug.WriteLine("Test " +  _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ServicePackNumber == servicePack).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion==majorVersion).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithReportMinor(long reportTypeId, int minorVersion)
        {
            // Debug.WriteLine("Test " +  _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ServicePackNumber == servicePack).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MinorVersion == minorVersion).ToList();
        }
        
         public IEnumerable<ReportFile> GetReportWithReportServicePack(long reportTypeId, int servicePack)
        {
            // Debug.WriteLine("Test " +  _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ServicePackNumber == servicePack).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.ServicePackNumber == servicePack).ToList();
        }
        
         public IEnumerable<ReportFile> GetReportWithReportBuildNumber(long reportTypeId, int buildNumber)
        {
            // Debug.WriteLine("Test " +  _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ServicePackNumber == servicePack).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithReportMajorMinor(long reportTypeId, int majorVersion,int minorVersion)
        {
            // Debug.WriteLine("Test " +  _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ServicePackNumber == servicePack).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion== minorVersion).ToList();
        }
        
        public IEnumerable<ReportFile> GetReportWithReportMajorService(long reportTypeId, int majorVersion, int servicePack)
        {
            // Debug.WriteLine("Test " +  _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ServicePackNumber == servicePack).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.ServicePackNumber == servicePack).ToList();
        }
        
            public IEnumerable<ReportFile> GetReportWithReportMajorBuild(long reportTypeId, int majorVersion, int buildNumber)
        {
            // Debug.WriteLine("Test " +  _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ServicePackNumber == servicePack).ToList());
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
                  public IEnumerable<ReportFile> GetReportWithReportMajorMinorService(long reportTypeId, int majorVersion, int minorVersion, int servicePack)
        {            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber== servicePack).ToList();
        }
        
       public IEnumerable<ReportFile> GetReportWithReportMajorMinorBuild(long reportTypeId, int majorVersion, int minorVersion, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
       public IEnumerable<ReportFile> GetReportWithReportMajorServiceBuild(long reportTypeId, int majorVersion, int servicePack, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
       public IEnumerable<ReportFile> GetReportWithReportMinorService(long reportTypeId, int minorVersion, int servicePack)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack).ToList();
        }
        
       public IEnumerable<ReportFile> GetReportWithReportMinorBuild(long reportTypeId, int minorVersion, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MinorVersion == minorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
         public IEnumerable<ReportFile> GetReportWithReportServiceBuild(long reportTypeId, int servicePack, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }

        public IEnumerable<ReportFile> GetReportWithReportMinorServiceBuild(long reportTypeId, int minorVersion, int servicePack, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber== servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }
    }
}
