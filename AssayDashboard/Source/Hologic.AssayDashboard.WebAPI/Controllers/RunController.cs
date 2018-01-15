using System.Web;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Web.Http;
using System.Xml;

namespace Hologic.AssayDashboard.WebAPI.Controllers
{
    public class RunController : ApiController
    {
        public class Pair
        {
            public Object run { get; set; }
            public string FileName { get; set; }
        }

        [HttpPost, Route("api/Run/ProcessRun")]
        public string processRun()
        {
            HttpPostedFile file = HttpContext.Current.Request.Files.Get(0);
            //var crcHelper = Hologic.Library.CrcHelper.CrcCalculator.crc16();
            var lib = new Hologic.Library.ExportReader.RunExportReader.RunExportReader();

            var fileBlob = new Byte[0];

            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileBlob = binaryReader.ReadBytes(file.ContentLength);
            }

            //temp path
            var tempPath = Path.GetTempPath();
            var tempPathFile = Path.Combine(tempPath, file.FileName);
            File.WriteAllBytes(tempPathFile, fileBlob);

            var toReturn = lib.DeSerializeRunExport(tempPathFile);

            //validate Crc
            var CrcHelper = new Hologic.Library.CrcHelper.XmlExportCrc();
            XmlDocument xmlToBeTested = new XmlDocument();
            xmlToBeTested.Load(tempPathFile);
            var result = CrcHelper.ValidateCrc(xmlToBeTested);
            
            //Crc needs to be fixed
            if (result.Result != Hologic.Library.CrcHelper.ResultsCrc.Results.Passed)
            {
                result = CrcHelper.RecalculateCrc(xmlToBeTested);
            }

            return JsonConvert.SerializeObject(new Pair
            {
                run = toReturn,
                FileName = file.FileName
            });
        }

    }
}