using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    [XmlRoot("ResultCategoryEnumeration")]
    public class ResultEnum
    {
        [XmlElement("ResultInterpretationValue")]
        public List<ResultInterpretation> interpretationList;
    }
}
