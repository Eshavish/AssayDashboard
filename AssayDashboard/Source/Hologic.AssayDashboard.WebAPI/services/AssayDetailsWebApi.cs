using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hologic.AssayDashboard.WebApi.services
{
    public class AssayDetailsWebApi
    {

        public IEnumerable<AssayDetails> AssayDetailsToService(string assayname, long typeid, long id)
        {
            AssayDetails assayInfo = new AssayDetails();
            assayInfo.AssayNameToComponent = assayname;
            assayInfo.TypeIdToComponent = typeid;
            assayInfo.IdToComponent = id;

            return null;
        }
    }
}