using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Domain.Services
{
    public class CurveDataPointService
    {
        public class Pair
        {
            public DataSet dataSet { get; set; }
            public string FileType { get; set; }
        }
        public class DataSet
        {
            public List<Channel> channels = new List<Channel>();
        }
        public class Channel
        {
            public string channel = "";

            //key is the sampleID and datapoint is the x and y
            public IDictionary<string, DataPoint> dict = new Dictionary<string, DataPoint>();
        }
        public class DataPoint
        {
            public List<int> xAxis = new List<int>();

            public List<int> yAxis = new List<int>();
        }

        public Pair getCurveData(Byte[] byteFile, string fileName)
        {
            DataSet dataSet = new DataSet();

            var tempPath = Path.GetTempPath();
            var tempPathFile = Path.Combine(tempPath, fileName);
            File.WriteAllBytes(tempPathFile, byteFile);

            using (var streamReader = new StreamReader(tempPathFile))
            {
                var firstLine = streamReader.ReadLine();
                var lowerFirstLine = firstLine.ToLower();

                //debugging
                var secondLine = streamReader.ReadLine();
                String[] seperatedValue2 = secondLine.Split(',');
                var thirdLine = streamReader.ReadLine();
                String[] seperatedValue3 = thirdLine.Split(',');

                if (lowerFirstLine.Contains("sampleid") &&
                   (lowerFirstLine.Contains("testorderid") || lowerFirstLine.Contains("testorder id")))
                {
                    //only TCycle
                    if (lowerFirstLine.Contains("cyclenum"))
                    {
                        return TCYCCurve(dataSet, streamReader);
                    }
                    else if (lowerFirstLine.Contains("rtf"))
                    {
                        return FluoCurve(dataSet, streamReader);
                    }
                }
            }
            return new Pair{
                dataSet = dataSet,
                FileType = ""
            };
        }

        public Pair FluoCurve(DataSet dataSet, StreamReader streamReader)
        {
            List<string> channelHolder = new List<string>();

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                String[] seperatedValue = line.Split(';');

                var channelName = seperatedValue[7];
                var sampleIDName = seperatedValue[5];

                //TimeStamp
                var xPoint = Int32.Parse(seperatedValue[0]);
                //Measurement Results
                var yPoint = Int32.Parse(seperatedValue[8]);

                //check if channel object is already created
                if (!channelHolder.Contains(channelName))
                {
                    Channel newChannel = new Channel();
                    newChannel.channel = channelName;

                    DataPoint newPoint = new DataPoint();
                    newPoint.xAxis.Add(xPoint);
                    newPoint.yAxis.Add(yPoint);

                    newChannel.dict.Add(sampleIDName, newPoint);
                    dataSet.channels.Add(newChannel);

                    channelHolder.Add(channelName);
                    continue;
                }
                for (int i = 0; i < dataSet.channels.Count; i++)
                {
                    //find the channel
                    if (dataSet.channels[i].channel == channelName)
                    {
                        //check if sampleID already exists
                        if (dataSet.channels[i].dict.ContainsKey(sampleIDName))
                        {
                            dataSet.channels[i].dict[sampleIDName].xAxis.Add(xPoint);
                            dataSet.channels[i].dict[sampleIDName].yAxis.Add(yPoint);
                        }
                        //does not exist
                        else
                        {
                            DataPoint newPoint = new DataPoint();
                            newPoint.xAxis.Add(xPoint);
                            newPoint.yAxis.Add(yPoint);

                            dataSet.channels[i].dict.Add(sampleIDName, newPoint);
                        }
                    }
                }
            }
            //getting rid of duplicate in x and y points
            for (int i = 0; i < dataSet.channels.Count; i++)
            {
                foreach (KeyValuePair<string, DataPoint> entry in dataSet.channels[i].dict)
                {
                    var duplicateCheck = entry.Value.xAxis[0];
                    //Get Rid of Duplicates
                    for (int j = 1; j < entry.Value.xAxis.Count; ++j)
                    {
                        if (duplicateCheck == entry.Value.xAxis[j])
                        {
                            duplicateCheck = entry.Value.xAxis[j];
                            entry.Value.xAxis.RemoveAt(j - 1);
                            entry.Value.yAxis.RemoveAt(j - 1);
                            --j;
                        }
                        else
                        {
                            duplicateCheck = entry.Value.xAxis[j];
                        }
                    }
                }
            }
            return new Pair { dataSet = dataSet, FileType = "Fluo"};
        }
        public Pair TCYCCurve(DataSet dataSet, StreamReader streamReader)
        {
            List<string> channelHolder = new List<string>();

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                String[] seperatedValue = line.Split(',');

                //checking Duplicates for x and y points
                if (seperatedValue.Length != 17)
                {
                    for (int i = 0; i < dataSet.channels.Count; i++)
                    {
                        foreach(KeyValuePair<string, DataPoint> entry in dataSet.channels[i].dict)
                        {
                            var duplicateCheck = entry.Value.xAxis[0];
                            //Get Rid of Duplicates
                            for (int j = 1; j < entry.Value.xAxis.Count; ++j)
                            {
                                if (duplicateCheck == entry.Value.xAxis[j])
                                {
                                    duplicateCheck = entry.Value.xAxis[j];
                                    entry.Value.xAxis.RemoveAt(j-1);
                                    entry.Value.yAxis.RemoveAt(j-1);
                                    --j;
                                }
                                else
                                {
                                    duplicateCheck = entry.Value.xAxis[j];
                                }
                            }
                        }
                    }
                    return new Pair { dataSet = dataSet, FileType = "TCYC" };
                }
                var channelName = seperatedValue[2];
                var sampleIDName = seperatedValue[12];

                //cycle num
                var xPoint = Int32.Parse(seperatedValue[0]);
                //fluo
                var yPoint = Int32.Parse(seperatedValue[1]);

                //check if channel object is already created
                if (!channelHolder.Contains(channelName)){
                    Channel newChannel = new Channel();
                    newChannel.channel = channelName;

                    DataPoint newPoint = new DataPoint();
                    newPoint.xAxis.Add(xPoint);
                    newPoint.yAxis.Add(yPoint);

                    newChannel.dict.Add(sampleIDName, newPoint);
                    dataSet.channels.Add(newChannel);

                    channelHolder.Add(channelName);
                    continue;
                }
                for (int i = 0; i < dataSet.channels.Count; i++)
                {
                    //find the channel
                    if (dataSet.channels[i].channel == channelName)
                    {
                        //check if sampleID already exists
                        if (dataSet.channels[i].dict.ContainsKey(sampleIDName))
                        {
                            dataSet.channels[i].dict[sampleIDName].xAxis.Add(xPoint);
                            dataSet.channels[i].dict[sampleIDName].yAxis.Add(yPoint);
                        }
                        //does not exist
                        else
                        {
                            DataPoint newPoint = new DataPoint();
                            newPoint.xAxis.Add(xPoint);
                            newPoint.yAxis.Add(yPoint);

                            dataSet.channels[i].dict.Add(sampleIDName, newPoint);
                        }
                    }
                }
            }
            return new Pair { dataSet = dataSet, FileType = "TCYC" };
        }
    }
}
