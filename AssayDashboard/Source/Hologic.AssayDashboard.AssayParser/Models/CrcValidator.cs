using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hologic.AssayDashboard.AssayParser
{
    public class CrcValidator
    {
        private const string ObjectCrcAttName = "ObjectCRC";

        public static ushort CalculateCrcFor(string xmlString)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            XmlNode crcNode = xmlDoc.SelectSingleNode(string.Format("./*[@{0}]", ObjectCrcAttName));
            XmlAttribute crcAttribute = crcNode.Attributes[ObjectCrcAttName];
            crcNode.Attributes.Remove(crcAttribute);
            xmlDoc.ReplaceChild(crcNode, xmlDoc.SelectSingleNode(crcNode.Name));

            ushort crcValue = CalculateCrc(xmlDoc);
            return crcValue;
        }

        private static ushort CalculateCrc(XmlDocument xmlDoc)
        {
            byte[] xmlbytes = Encoding.UTF8.GetBytes(xmlDoc.OuterXml);
            return crc16(xmlbytes, xmlbytes.Length);
        }

        private static ushort crc16(byte[] data, int length)
        {
            byte i;
            uint ptr = 0;
            ushort value;
            ushort crc = 0xffff;
            const ushort POLY = 0x8408;

            if (length == 0)
            {
                crc = (ushort)(~(int)crc);
            }
            else
            {
                do
                {
                    for (i = 0, value = data[ptr++]; i < 8; i++, value >>= 1)
                    {
                        if (((ushort)(crc & 0x0001) ^ (ushort)(value & 0x0001)) != 0)
                            crc = (ushort)((crc >> 1) ^ POLY);
                        else
                            crc >>= 1;
                    }
                }
                while (--length != 0);

                uint temp = (~(uint)crc);
                crc = (ushort)(temp & 0xffff);
                value = crc;
                crc &= 0x00ff;
                crc = (ushort)((crc << 8) | ((value >> 8) & 0xff));
            }
            return crc;
        }
    }
}
