using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    public class Reagent
    {
        [XmlElement("ReagentName")]
        public string reagentName;

        [XmlElement("ReagentTypeID")]
        public string reagentTypeID;

        [XmlElement("IsOnboard")]
        public string isOnboard;

        [XmlElement("MaximumOpenKitStability")]
        public string openKitStability;

        [XmlElement("MaximumOnBoardStability")]
        public string onBoardStability;

        [XmlElement("FluidVessel")]
        public FluidVessel fluidVessel;

        public Reagent()
        {
            this.reagentName = "";
            this.reagentTypeID = "";
            this.openKitStability = "";
            this.onBoardStability = "";
            this.isOnboard = "";
            this.fluidVessel = null;
        }
    }
}
