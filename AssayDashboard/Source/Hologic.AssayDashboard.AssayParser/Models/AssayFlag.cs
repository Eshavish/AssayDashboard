using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    public class AssayFlag
    {
        [XmlAttribute("AssayFlagName")]
        public string flagName;

        [XmlAttribute("ReportedResultBit")]
        public string resultBit;

        [XmlAttribute("AssayFlagID")]
        public string flagId;

        [XmlAttribute("AssayFlagSeverity")]
        public string flagSeverity;

        [XmlElement("FlagLegend")]
        public string flagLegend;
    }
}