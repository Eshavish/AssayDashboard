using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    public class ResultInterpretation
    {
        [XmlElement("Interpretation")]
        public string interpretation;

        [XmlElement("DisplayStyle")]
        public string displayStyle;

        [XmlElement("DataReductionIndex")]
        public string reductionIndex;

        [XmlElement("IsLISHoldReleaseInterpretation")]
        public string lisHold;

        [XmlElement("ResultInterpretationConcept")]
        public string concept;

        public ResultInterpretation()
        {
            this.interpretation = "";
            this.displayStyle = "";
            this.reductionIndex = "";
            this.lisHold = "";
            this.concept = "";
        }
    }
}