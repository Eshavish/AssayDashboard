using Hologic.AssayDashboard.Database.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  

namespace Hologic.AssayDashboard.Database.Repositories
{
    public class AssayRepository : IDisposable
    {
        private readonly AssayDashboardContext _dataContext;
        public AssayRepository()
        {
            _dataContext = new AssayDashboardContext();
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public long GetAssayID(string assayName)
        {
            return _dataContext.AssayType.Where(aT => aT.AssayName == assayName).ToList()
                .ElementAt<AssayType>(0).TypeId;
        }
        public long GetAssayTypeId (string assayName)
        {
            return _dataContext.AssayType.Where(aT => aT.AssayName == assayName).ToList()
                .ElementAt<AssayType>(0).TypeId;
        }
        public string GetAssayNameUsingTypeID (long TypeID)
        {
            var result = _dataContext.AssayType.Where(aT => aT.TypeId == TypeID).ToList();
            if (result.Count()!= 0)
            {
                return result.ElementAt<AssayType>(0).AssayName;
            }
            else
            {
                return "";
            }
        }

        public AssayType GetAssayUsingTypeID(long TypeID)
        {
            var result = _dataContext.AssayType.Where(aT => aT.TypeId == TypeID).ToList();
            if (result.Count() != 0)
            {
                return result.ElementAt<AssayType>(0);
            }
            else
            {
                return null;
            }
        }
        public string GetAssayName(long ID)
        {
            return _dataContext.AssayType.Where(aT => aT.ID == ID).ToList().ElementAt<AssayType>(0).AssayName;
        }
        public long GetTypeIdWithID(long id)
        {
            return _dataContext.AssayType.Where(aT => aT.ID == id).ToList()
                .ElementAt<AssayType>(0).TypeId;
        }
        public IEnumerable<AssayType> GetAssay()
        {
            return _dataContext.AssayType.ToList();
        }
        public IEnumerable<ReportFile> GetReport()
        {
            return _dataContext.ReportFile.ToList();
        }
        public IEnumerable<ReportFile> GetAssayNameFromReportFile()
        {  
            return _dataContext.ReportFile.ToList();
        }
    }
}
