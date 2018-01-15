using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Http;

namespace Hologic.CurveRepo.WebAPI.Controllers
{
    public class LoginController : ApiController
    { 
        [HttpGet, Route("api/Login/{ID}/{PassWord}")]
        public string login(string ID, string PassWord)
        {
            Boolean isValid = false;

            var domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domainName))
            {
                // validate the credentials
                isValid = pc.ValidateCredentials(ID, PassWord);
            }

            return JsonConvert.SerializeObject(isValid);
        }
    }
}