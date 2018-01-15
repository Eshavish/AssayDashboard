using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    public class Setting
    {
        [XmlAttribute("name")]
        public string settingName;

        [XmlAttribute("value")]
        public string settingValue;
    }
}