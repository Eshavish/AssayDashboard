using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hologic.AssayDashboard.WebAPI.services
{
    public class SSVClass
    {
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int ServiceBuild { get; set; }
        public int BuildNumber { get; set; }

        public SSVClass(int majorVersion, int minorVersion, int serviceBuild, int buildNumber)
        {
            this.MajorVersion = majorVersion;
            this.MinorVersion = minorVersion;
            this.ServiceBuild = serviceBuild;
            this.BuildNumber = buildNumber;
        }
    }
}