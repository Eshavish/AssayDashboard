using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    public class Calibrator
    {
        [XmlElement("ReagentName")]
        public string calibratorName;

        [XmlElement("ReagentTypeID")]
        public string calibratorTypeID;

        [XmlElement("Replicates")]
        public string replicates;
        public Calibrator()
        {
            this.calibratorName = "";
            this.calibratorTypeID = "";
            this.replicates = "";
        }
    }
}
