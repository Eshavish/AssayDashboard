using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hologic.AssayDashboard.WebApi.services
{
    public class DisplayClass
    {
        public long Id { get; set; }
        public long TypeId { get; set; }
        public string AssayName { get; set; }
    }
}