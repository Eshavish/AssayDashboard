using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    [XmlRoot("DataReductionMethod")]
    public class DataReduction
    {
        [XmlArray("ResultCategories")]
        public List<ResultCategory> resultCategories;

        [XmlElement("DataReductionAssembly")]
        public string reductionAssembly;

        [XmlArray("AssayFlags")]
        public List<AssayFlag> assayFlags;

        [XmlArray("AssaySettings")]
        public List<Setting> assaySettings;

        [XmlArray("AssayDilutions")]
        public List<AssayDilution> assayDilutions;
    }
}
