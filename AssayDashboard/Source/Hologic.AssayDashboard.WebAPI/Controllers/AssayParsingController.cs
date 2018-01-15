using Hologic.AssayDashboard.AssayParser;
using System.IO;
using System.Web;
using System.Web.Http;

namespace Hologic.AssayDashboard.WebAPI.Controllers
{
    public class AssayParsingController: ApiController
    {
        private static readonly log4net.ILog logger = log4net.LogManager
        .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost, Route("api/Parsing/parseFile")]
        public string parseAssay()
        {
            HttpPostedFile file = HttpContext.Current.Request.Files.Get(0);

            using (var reader = new StreamReader(file.InputStream))
            {
                var xmlText = reader.ReadToEnd();
                logger.Info("Attempting to parse " + file.FileName);
                return AssaySerializer.createAssay(xmlText);
            }
        }
    }
}