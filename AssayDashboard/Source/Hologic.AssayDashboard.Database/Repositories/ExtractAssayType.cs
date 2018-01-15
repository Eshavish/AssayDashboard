using Hologic.AssayDashboard.Database.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Hologic.AssayDashboard.Database.AssayDashboardContext;

namespace Hologic.AssayDashboard.Database.repositories
{
    public class ExtractAssayType:IDisposable
    {
        private readonly AssayDashboardContext _dataContext;
        public ExtractAssayType()
        {
            _dataContext = new AssayDashboardContext();
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }
        public long GetAssayID(int typeIdToSend)
        {    
                Debug.WriteLine("Test " + _dataContext.AssayType.Where(aT => aT.TypeId == typeIdToSend).ToList()
                             .ElementAt<AssayType>(0).ID);
            var idFromAssayType = _dataContext.AssayType.Where(assayTypeTable => assayTypeTable.TypeId == typeIdToSend)
                .ToList().ElementAt<AssayType>(0).ID;
                return idFromAssayType;  
        }
        
        // report assay and major version given
        public IEnumerable<ReportFile> GetReportWithReportAssayMajorVersion(long id,long reportTypeId, int majorVersion)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion== majorVersion).ToList();
        }
        // report assay and build number
        public IEnumerable<ReportFile> GetReportWithReportAssayBuildNumber(long id, long reportTypeId, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        // report assay and servicePack
        public IEnumerable<ReportFile> GetReportWithReportAssayServicePack(long id, long reportTypeId, int servicePack)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.ServicePackNumber == servicePack).ToList();
        }
        // report assay and minor version
        public IEnumerable<ReportFile> GetReportWithReportAssayMinor(long id, long reportTypeId, int minorVersion)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MinorVersion == minorVersion).ToList();
        }
        // report assay minor version major version
        public IEnumerable<ReportFile> GetReportWithReportAssayMinorMajor(long id, long reportTypeId,int majorVersion ,int minorVersion)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion== minorVersion).ToList();
        }
        
        // report assay service pack major version
        public IEnumerable<ReportFile> GetReportWithReportAssayMajorServicePack(long id, long reportTypeId, int majorVersion, int servicePack)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.ServicePackNumber == servicePack).ToList();
        }
        
        // report assay build major version
        public IEnumerable<ReportFile> GetReportWithReportAssayMajorBuildNumber(long id, long reportTypeId, int majorVersion, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        // report assay service minor version
        public IEnumerable<ReportFile> GetReportWithReportAssayMinorService(long id, long reportTypeId, int minorVersion, int servicePack)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber == servicePack).ToList();
        }
        
        // report assay build minor version
        public IEnumerable<ReportFile> GetReportWithReportAssayMinorBuild(long id, long reportTypeId, int minorVersion, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MinorVersion == minorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        // report assay build service
        public IEnumerable<ReportFile> GetReportWithReportAssayBuildService(long id, long reportTypeId, int servicePack, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        // report assay major minor service
        public IEnumerable<ReportFile> GetReportWithReportAssayMajorMinorService(long id, long reportTypeId, int majorVersion, int minorVersion,int servicePack)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.ServicePackNumber== servicePack).ToList();
        }
        
        // report assay major minor build
        public IEnumerable<ReportFile> GetReportWithReportAssayMajorMinorBuild(long id, long reportTypeId, int majorVersion, int minorVersion, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.MinorVersion == minorVersion && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        // report assay minor service build
        public IEnumerable<ReportFile> GetReportWithReportAssayMinorServiceBuild(long id, long reportTypeId, int minorVersion, int servicePack,int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MinorVersion == minorVersion &&  reportFileTable.ServicePackNumber== servicePack  && reportFileTable.BuildNumber == buildNumber).ToList();
        }
        
        // report assay minor service build
        public IEnumerable<ReportFile> GetReportWithReportAssayMajorServiceBuild(long id, long reportTypeId, int majorVersion, int servicePack, int buildNumber)
        {
            return _dataContext.ReportFile.Where(reportFileTable => reportFileTable.AssayTypeId == id && reportFileTable.ReportTypeId == reportTypeId && reportFileTable.MajorVersion == majorVersion && reportFileTable.ServicePackNumber == servicePack && reportFileTable.BuildNumber == buildNumber).ToList();
        }
    }
    
}
