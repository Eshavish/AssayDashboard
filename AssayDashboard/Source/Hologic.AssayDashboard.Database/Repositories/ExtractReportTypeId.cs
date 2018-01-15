using Hologic.AssayDashboard.Database.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Database.repositories
{
    public class ExtractReportTypeId
    {
        private readonly AssayDashboardContext _dataContext;
        public ExtractReportTypeId()
        {
            _dataContext = new AssayDashboardContext();
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }
        public long GetReportTypeId(string reportType)
        {
            Debug.WriteLine("Test " + _dataContext.ReportType.Where(reportTypesTable => reportTypesTable.ReportName == reportType.Trim()).ToList()
                         .ElementAt<ReportType>(0).Id);
            var idFromReportType = _dataContext.ReportType.Where(reportTypesTable => reportTypesTable.ReportName == reportType.Trim()).ToList()
                         .ElementAt<ReportType>(0).Id;
            return idFromReportType;
        }

    }
}
