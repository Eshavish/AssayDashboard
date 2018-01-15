// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------

using Hologic.AssayDashboard.Database.Models;
using Hologic.AssayDashboard.Database.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;


namespace Hologic.AssayDashboard.Domain.Services
{
    public class FileUploadService
    {
  
        public CurveFile Transform (string jsonMessages, string AssayName)
        {
            var delim = new[] { '"' };
            jsonMessages = Regex.Unescape(jsonMessages);
            jsonMessages = jsonMessages.TrimEnd(delim).TrimStart(delim);

            AssayName = Regex.Unescape(AssayName);
            AssayName = AssayName.TrimEnd(delim).TrimStart(delim);

            dynamic data = JObject.Parse(jsonMessages);

            long AssayDBID = 0;

            using (var repo = new AssayRepository())
            {
                AssayDBID = repo.GetAssayID(AssayName);
            }

            CurveFile toReturn = JsonConvert.DeserializeObject<CurveFile>(jsonMessages);
            toReturn.AssayDBID = AssayDBID;

            //Extract extension from file name
            ExtractExtensionService helper = new ExtractExtensionService();
            string[] fullFileName = new string[2];
            fullFileName = helper.ExtractExt(toReturn);

            return toReturn;
        }

        public string getVersionNum(string indicator, Byte[] byteFile, string fileName)
        {
            var tempPath = Path.GetTempPath();
            var tempPathFile = Path.Combine(tempPath, fileName);
            File.WriteAllBytes(tempPathFile, byteFile);

            using (var streamReader = new StreamReader(tempPathFile))
            {
                var firstLine = streamReader.ReadLine();
                var secondLine = streamReader.ReadLine();

                if (firstLine == null || secondLine == null)
                {
                    return "";
                }

                var lowerFirstLine = firstLine.ToLower();

                string[] seperatedValueFirst = lowerFirstLine.Split(',');
                string[] seperatedValueSecond = secondLine.Split(',');

                if (lowerFirstLine.Contains("sampleid") &&
                   (lowerFirstLine.Contains("testorderid") || lowerFirstLine.Contains("testorder id")))
                {
                    //RPTool files
                    if (seperatedValueFirst.Length == 9)
                    {
                        return "";
                    }
                    //only TCycle for now
                    if (lowerFirstLine.Contains("cyclenum"))
                    {
                        if (indicator == "AssayVersion") {
                            //to differentiate TCYC and new .curve files
                            if (seperatedValueFirst[4] == "rfutimestamp") {
                                return seperatedValueSecond[7];
                            }
                            //.curve
                            return seperatedValueSecond[8];
                        }
                        return "";
                    }
                    //Fluorescence curve
                    else if (lowerFirstLine.Contains("rtf"))
                    {
                        return "";
                    }
                }
                //validates FLUO.csv file
                else if (seperatedValueFirst.Length == 19)
                {
                    return "";
                }
                //validates LUMI.csv file
                else
                {
                    if (seperatedValueFirst.Length > 5)
                    {
                        var numOfDataPts = seperatedValueFirst[5];
                        
                        if (indicator == "SoftwareVersion")
                        {
                            return seperatedValueFirst[Int32.Parse(numOfDataPts) + 12];
                        }
                        else if (indicator =="AssayVersion")
                        {
                            return seperatedValueFirst[Int32.Parse(numOfDataPts) + 11];
                        }
                    }
                }
            }
            return "";
        }


    }
}
