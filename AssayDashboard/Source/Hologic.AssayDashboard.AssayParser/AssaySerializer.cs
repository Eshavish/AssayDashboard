using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Hologic.AssayDashboard.AssayParser
{
    public class AssaySerializer
    {
        private static readonly log4net.ILog logger = log4net.LogManager
        .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string createAssay(string xmlText)
        {
            var assay = new Assay();
            var serializer = new XmlSerializer(typeof(Assay));

            using (var xmlReader = XmlReader.Create(new StringReader(xmlText)))
            {
                try
                {
                    assay = (Assay)serializer.Deserialize(xmlReader);
                    assay.calculatedCrc = CrcValidator.CalculateCrcFor(xmlText);
                    ProcessAssay(assay, xmlText);

                    var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string jsonString = javaScriptSerializer.Serialize(assay);
                    logger.Info("Parsed text successfully");
                    return jsonString;
                }
                catch (Exception e)
                {
                    logger.Warn("Unable to parse string");
                    return null;
                }
            }
        }

        private static Assay ProcessAssay(Assay assay, string xmlText)
        {
            string analytes = "";
            foreach (Analyte analyte in assay.analyteList)
            {
                analytes += analyte.analyteName + ", ";
            }
            if(assay.calculatedCrc != assay.objectCrc)
            {
                assay.xmlText = xmlText;
            }
            assay.analytes = analytes.Trim(',', ' ');
            assay.getType();
            assay.processSteps = GetProcessSteps(xmlText);
            SortAssay(assay);
            return assay;
        }

        private static List<Dictionary<string, Object>> GetProcessSteps(string xmlText)
        {
            var doc = XDocument.Parse(xmlText);
            var steplist = new List<Dictionary<string, Object>>();
            var step = new Dictionary<string, Object>();

            foreach (var element in doc.Root.Descendants("ProcessSteps").Elements())
            {
                step = new Dictionary<string, Object>();
                step["Step"] = element.Name.ToString();
                foreach (var node in element.Elements())
                {
                    if (node.Name.ToString().Contains("StepList"))
                    {
                        step[node.Name.ToString()] = GetStepList(node);
                    }
                    else
                    {
                        step[node.Name.ToString()] = node.Value.Trim();
                    }
                }
                steplist.Add(step);
            }
            return steplist;
        }

        private static List<Dictionary<string, string>> GetStepList(XElement node)
        {
            var innerList = new List<Dictionary<string, string>>();
            var innerStep = new Dictionary<string, string>();
            foreach (var n in node.Descendants())
            {
                if (n.HasElements)
                {
                    innerStep = new Dictionary<string, string>();
                    innerStep["Step"] = n.Name.ToString();
                    foreach (var c in n.Descendants())
                    {
                        innerStep[c.Name.ToString()] = c.Value.Trim();
                    }
                    innerList.Add(innerStep);
                }
            }
            return innerList;
        }

        private static void SortAssay(Assay assay)
        {
            assay.dataReduction.resultCategories = 
                assay.dataReduction.resultCategories.OrderBy(
                    i => i.index).ToList();

            assay.dataReduction.resultCategories.ForEach(i => {
                i.resultEnum.interpretationList = i.resultEnum.interpretationList.OrderBy(
                    x => x.reductionIndex).ToList();
            });

            assay.reagentTypeDescription.reagents = 
                assay.reagentTypeDescription.reagents.OrderBy(v => v.isOnboard).ToList();
            assay.reagentTypeDescription.reagents.Reverse(); 
        }
    }
}
