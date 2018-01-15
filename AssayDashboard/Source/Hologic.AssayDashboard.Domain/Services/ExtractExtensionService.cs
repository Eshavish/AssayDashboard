using Hologic.AssayDashboard.Database.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Domain.Services
{
    public class ExtractExtensionService
    {
        public string[] ExtractExt( CurveFile curveObject)
        {
            string extension = Path.GetExtension(curveObject.FullFileName);
            string fileName = Path.GetFileNameWithoutExtension(curveObject.FullFileName);

            string[] toReturn = new string[2];
            toReturn[0] = fileName;
            toReturn[1] = extension;

            return toReturn;
        }
    }
}
