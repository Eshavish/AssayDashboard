using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Domain.Services
{
    public class DownloadFileService
    {
        public string DownloadFromByte (byte[] byteFile, string fileName)
        {
            var tempPath = Path.GetTempPath();
            var tempPathFile = Path.Combine(tempPath, fileName);
            File.WriteAllBytes(tempPathFile, byteFile);

            return tempPathFile;
        }
    }
}
