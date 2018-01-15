using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    public class ResultCategory
    {
        [XmlElement("ResultCategoryName")]
        public string categoryName;

        [XmlElement("ResultCategoryIndex")]
        public int index;

        [XmlElement("IsSentToLIS")]
        public string sentToLIS;

        [XmlElement("IsSentToUI")]
        public string sentToUI;

        [XmlElement("IsSentToReport")]
        public string sentToReport;

        [XmlElement("IsLISHoldReleaseInterpretation")]
        public string lisHold;

        [XmlElement("IsIncludedInSpecimenTallies")]
        public string inSpecimenTallies;

        [XmlElement("ResultCategoryEnumeration")]
        public ResultEnum resultEnum;
        public bool active;

        public ResultCategory()
        {
            this.categoryName = "";
            this.index = 0;
            this.sentToLIS = "";
            this.sentToUI = "";
            this.sentToReport = "";
            this.lisHold = "";
            this.inSpecimenTallies = "";
            this.active = false; 
        }
    }
}
