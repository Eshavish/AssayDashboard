using Hologic.AssayDashboard.Database.Models;
using Hologic.AssayDashboard.Database.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Domain.Services
{
    public class FileValidationService
    {
        public class PairResponse{
            public Boolean Indicator { get; set; }
            public string message { get; set; }
        }

        public string getAssayFromFile(Byte[] byteFile, string fileName)
        {
            var delim = new[] { '"' };
            fileName = Regex.Unescape(fileName);
            fileName = fileName.TrimEnd(delim).TrimStart(delim);

            var tempPath = Path.GetTempPath();
            var tempPathFile = Path.Combine(tempPath, fileName);
            File.WriteAllBytes(tempPathFile, byteFile);

            var repo = new AssayRepository();

            var typeID = "";

            using (var streamReader = new StreamReader(tempPathFile))
            {
                var firstLine = streamReader.ReadLine();
                var secondLine = streamReader.ReadLine();

                if (firstLine == null || secondLine == null)
                {
                    return "Invalid";
                }

                var lowerFirstLine = firstLine.ToLower();

                string[] seperatedValueFirst = lowerFirstLine.Split(',');

                if (lowerFirstLine.Contains("sampleid") &&
                   (lowerFirstLine.Contains("testorderid") || lowerFirstLine.Contains("testorder id")))
                {
                    //RPTool files
                    if (seperatedValueFirst.Length == 9)
                    {
                        List<string> firstColHolder = new List<string>();
                        var newStream = new StreamReader(tempPathFile);
                        while (!newStream.EndOfStream)
                        {
                            var newLine = newStream.ReadLine();
                            String[] seperatedVal = newLine.Split(',');
                            firstColHolder.Add(seperatedVal[0]);
                        }

                        //check the string with number
                        for (var index = 0; index < firstColHolder.Count; index++)
                        {
                            var charArr = firstColHolder[index].ToCharArray();
                            if (Char.IsNumber(charArr[0]))
                            {

                                typeID = charArr[0].ToString();
                                typeID = typeID.Insert(1, charArr[1].ToString());
                                var toReturn = repo.GetAssayNameUsingTypeID(Int32.Parse(typeID));
                                return toReturn;
                            }
                        }
                    }
                    //TCycle and some versions of .curve
                    if (lowerFirstLine.Contains("cyclenum"))
                    {
                        String[] seperatedValue = secondLine.Split(',');
                        //TCYC
                        if (seperatedValue.Length != 15)
                        {
                            typeID = seperatedValue[7];
                        }
                        //.Curve
                        else
                        {
                            typeID = seperatedValue[6];
                        }
                        var toReturn = repo.GetAssayNameUsingTypeID(Int32.Parse(typeID));
                        //If assay are outdated
                        if (toReturn == "")
                        {
                            return "Invalid-Outdated";
                        }
                        return toReturn;
                    }
                    //Fluorescence curve
                    else if (lowerFirstLine.Contains("rtf"))
                    {
                        String[] seperatedValue = secondLine.Split(';');

                        var Assay = seperatedValue[2];

                        String[] assayNumber = seperatedValue[2].Split(',');

                        var AssayDBID = assayNumber[0];

                        var toReturn = repo.GetAssayNameUsingTypeID(Int32.Parse(AssayDBID));
                        //If assay are outdated
                        if (toReturn == "")
                        {
                            return "Invalid-Outdated";
                        }
                        return toReturn;
                    }
                }
                //validates FLUO.csv file
                else if (seperatedValueFirst.Length == 19)
                {

                    var typeIDStr = seperatedValueFirst[8];
                    var toReturn = repo.GetAssayNameUsingTypeID(Int32.Parse(typeIDStr));
                    //If assay are outdated
                    if (toReturn == "")
                    {
                        return "Invalid-Outdated";
                    }
                    return toReturn;
                }
                //validates LUMI.csv file
                else
                {
                    var typeIDStr = "";
                    if (seperatedValueFirst.Length > 5)
                    {
                        if (seperatedValueFirst[5] != null)
                        {
                            var numOfDataPts = seperatedValueFirst[5];
                            typeIDStr = seperatedValueFirst[Int32.Parse(numOfDataPts) + 8];
                        }
                        else
                        {
                            return "Invalid";
                        }
                        var toReturn = repo.GetAssayNameUsingTypeID(Int32.Parse(typeIDStr));
                        //If assay are outdated
                        if (toReturn == "")
                        {
                            return "Invalid-Outdated";
                        }
                        return toReturn;
                    }
                }
            }
            return "Invalid";
        }

        public PairResponse ValidateFile(Byte [] byteFile, string fileName, string AssayName)
        {
            var delim = new[] { '"' };
            AssayName = Regex.Unescape(AssayName);
            AssayName = AssayName.TrimEnd(delim).TrimStart(delim);

            var tempPath = Path.GetTempPath();
            var tempPathFile = Path.Combine(tempPath, fileName);
            File.WriteAllBytes(tempPathFile, byteFile);
            
            using (var streamReader = new StreamReader(tempPathFile))
            {
                var firstLine = streamReader.ReadLine();
                var secondLine = streamReader.ReadLine();
                var lowerFirstLine = firstLine.ToLower();
                string lowerSecondLine = "";
                if (secondLine != null)
                {
                    lowerSecondLine = secondLine.ToLower();
                }
                else
                {
                    return new PairResponse { Indicator = false, message = "^Curve File has no content" };
                }
                long TypeId;

                //parse
                String[] seperatedValue = firstLine.Split(',');

                //validates TCYC, .curve, LUMI.RPTool, FLUO.RPTool
                if (lowerFirstLine.Contains("sampleid") &&
                    (lowerFirstLine.Contains("testorderid") || lowerFirstLine.Contains("testorder id"))){

                    using (var repo = new AssayRepository())
                    {
                        TypeId = repo.GetAssayTypeId(AssayName);
                    }
                    //RPTool files
                    if (seperatedValue.Length == 9)
                    {
                        List<string> firstColHolder = new List<string>(); 
                        var newStream = new StreamReader(tempPathFile);
                        while (!newStream.EndOfStream)
                        {
                            var newLine = newStream.ReadLine();
                            String[] seperatedVal = newLine.Split(',');
                            firstColHolder.Add(seperatedVal[0]);
                        }

                        //check the string with number
                        for (var index = 0; index<firstColHolder.Count; index++)
                        {
                            var charArr = firstColHolder[index].ToCharArray();
                            if (Char.IsNumber(charArr[0]))
                            {
                                //TODO get index 0 and 1 and combine them 
                                string typeID = charArr[0].ToString();
                                typeID = typeID.Insert(1, charArr[1].ToString());
                                
                                return new PairResponse { Indicator = true, message = "" };
                            }
                        }
                        return new PairResponse { Indicator = false, message = "^Curve does not match Assay type" };
                    }
                    //TCycle curve
                    if (lowerFirstLine.Contains("cyclenum")){
                        if (TypeId > 99)
                        {
                            if (lowerSecondLine.Contains(AssayName.ToLower()) && lowerSecondLine != null)
                            {
                                return new PairResponse { Indicator = true, message = "" };
                            }
                        }
                        return new PairResponse { Indicator = false, message = "^Curve does not match Assay type" };
                    //Fluorescence curve
                    }
                    else if (lowerFirstLine.Contains("rtf"))
                    {
                        if (TypeId < 100 && lowerSecondLine != null)
                        {
                            return new PairResponse { Indicator = true, message = "" };
                        }
                        else
                        {
                            return new PairResponse { Indicator = false, message = "^Curve does not match Assay type" };
                        }
                    }
                    //Else
                    else if (lowerFirstLine.Contains("rfu"))
                    {
                        if (TypeId < 100 && lowerSecondLine != null)
                        {
                            return new PairResponse { Indicator = true, message = "" };
                        }
                        else
                        {
                            return new PairResponse { Indicator = false, message = "^Curve does not match Assay type" };
                        }
                    }
                }
                //validates FLUO.csv file
                else if (seperatedValue.Length == 19)
                {
                    //TODO validate if mapped correctly
                    var typeIDStr = seperatedValue[8];
                    return new PairResponse { Indicator = true, message = "" };
                }
                //validates LUMI.csv file
                else
                {
                    var typeIDStr = "";
                    if (seperatedValue.Length > 5)
                    {
                        if (seperatedValue[5] != null)
                        {
                            var numOfDataPts = seperatedValue[5];
                            typeIDStr = seperatedValue[Int32.Parse(numOfDataPts) + 8];
                            return new PairResponse { Indicator = true, message = "" };
                        }
                        else
                        {
                            return new PairResponse { Indicator = false, message = "^Invalid Curve File" };
                        }
                    }
                }
            }
            return new PairResponse { Indicator = false, message = "^Invalid Curve File" };
        }
    }
}
