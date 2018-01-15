using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    [XmlRoot("ReagentTypeDescriptions")]
    public class ReagentTypeDescription
    {
        [XmlElement("CalibratorTypeDescription")]
        public List<Calibrator> calibrators;

        [XmlElement("ControlTypeDescription")]
        public List<Control> controls;

        [XmlElement("AssayReagentTypeDescription")]
        public List<Reagent> reagents;
    }
}
