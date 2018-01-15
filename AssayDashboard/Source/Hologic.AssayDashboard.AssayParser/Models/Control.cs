using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    public class Control
    {
        [XmlElement("ReagentName")]
        public string controlName;

        [XmlElement("MaximumOnBoardStability")]
        public string onBoardStability;

        [XmlElement("ReagentTypeID")]
        public string reagentTypeID;

        public Control()
        {
            this.controlName = "";
            this.onBoardStability = "";
            this.reagentTypeID = "";
        }
    }
}
