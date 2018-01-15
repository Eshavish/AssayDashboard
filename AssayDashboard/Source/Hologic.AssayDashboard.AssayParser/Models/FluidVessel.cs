using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    public class FluidVessel
    {
        [XmlElement("NumberOfTests")]
        public int numberOfTests;

        public FluidVessel()
        {
            this.numberOfTests = 0;
        }
    }
}