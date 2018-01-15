using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    [XmlRoot("AssayDescription")]
    public class Assay
    {
        [XmlElement("AssayShortName")]
        public string assayName;

        [XmlElement("Version")]
        public string version;

        [XmlElement("AssayTypeID")]
        public string assayTypeID;

        [XmlElement("AssayStatus")]
        public string assayStatus;

        [XmlElement("ThermalProfileReference")]
        public string thermalProfileReference;

        [XmlElement("FrontEndReference")]
        public string frontEndReference;

        [XmlElement("IsFrontEnd")]
        public string isFrontEnd;

        [XmlElement("IsLocked")]
        public string isLocked;

        [XmlArray("AnalyteList")]
        public List<Analyte> analyteList;

        [XmlArray("AssayMessages")]
        public List<AssayMessage> assayMessages;

        [XmlElement("ReagentTypeDescriptions")]
        public ReagentTypeDescription reagentTypeDescription;

        [XmlIgnore]
        public List<Dictionary<string, Object>> processSteps;

        public ushort calculatedCrc;
        public string type;
        public string analytes;
        public string xmlText;

        [XmlElement("DataReductionMethod")]
        public DataReduction dataReduction;

        [XmlAttribute("ObjectCRC")]
        public ushort objectCrc;

        [XmlAttribute("Resource")]
        public string resource;

        public Assay()
        {
            this.assayName = "";
            this.version = "";
            this.assayTypeID = "";
            this.assayStatus = "";
            this.thermalProfileReference = "";
            this.frontEndReference = "";
            this.isFrontEnd = "";
            this.isLocked = "";
            this.type = "";
            this.analytes = "";
            this.xmlText = "";
            this.analyteList = new List<Analyte>();
            this.assayMessages = new List<AssayMessage>();
            this.reagentTypeDescription = null;
            this.dataReduction = null;
        }

        public void getType() {
            if (this.isLocked == "true" && this.frontEndReference.Length == 0 && this.isFrontEnd.Length == 0)
            {
              this.type = "TMA";
            }
            if (this.isLocked == "true" && this.frontEndReference.Length != 0 && this.assayTypeID.Length <= 3)
            {
              this.type = "PCR";
            }
            if (this.isLocked == "true" && this.isFrontEnd == "true")
            {
              this.type = "Front-end";
            }
            if (this.assayTypeID.Length > 3 && this.isLocked == "false")
            {
              this.type = "Open Access";
            }
        }
    }
}
