using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    public class AssayDilution
    {
        [XmlAttribute("DilFlagID")]
        public string flagId;

        [XmlAttribute("DilFactor")]
        public string factor;
    }
}