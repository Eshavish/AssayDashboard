using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    [XmlRoot("AssayMessages")]
    public class AssayMessage
    {
        [XmlElement("Text")]
        public string message;

        [XmlAttribute("EventCode")]
        public string eventCode;

        [XmlAttribute("Diagnostic")]
        public string diagnostic;

        [XmlAttribute("Type")]
        public string type;

        public AssayMessage()
        {
            this.message = "";
            this.eventCode = "";
            this.diagnostic = "";
            this.type = "";
        }
    }
}
