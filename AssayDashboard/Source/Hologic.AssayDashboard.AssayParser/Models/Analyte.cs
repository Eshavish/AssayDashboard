using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    [XmlRoot("AnalyteList")]
    public class Analyte
    {
        [XmlElement("AnalyteName")]
        public string analyteName;
        public Analyte()
        {
            this.analyteName = "";
        }
    }
}
