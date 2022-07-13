using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KMS.Helper;
using KMS.Management.Model;
using System;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Oracle.ManagedDataAccess.Client;
using System.Net;
using System.Text;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using KMS.Master.Model;

namespace KMS.API.Controllers
{
    public class MasterController : APIController
    {
        [ActionName("server")]
        [HttpGet]
        public IHttpActionResult Server()
        {
            var user = new M_User();
            if (isAuthorized(out user))
            {
                var data = M_Server.API.GetAll();

                return Json(new
                {
                    code = HttpStatusCode.OK,
                    data,
                });
            }

            return Content(HttpStatusCode.Forbidden, new
            {
                code = HttpStatusCode.Forbidden,
                status = "error",
                message = "Bearer token not valid"
            });

        }

        [ActionName("vendor")]
        [HttpGet]
        public IHttpActionResult Vendor()
        {
            var user = new M_User();
            if (isAuthorized(out user))
            {
                var data = M_Vendor.API.GetAll();

                return Json(new
                {
                    code = HttpStatusCode.OK,
                    data,
                });
            }

            return Content(HttpStatusCode.Forbidden, new
            {
                code = HttpStatusCode.Forbidden,
                status = "error",
                message = "Bearer token not valid"
            });
        }

        [ActionName("contract")]
        [HttpGet]
        public IHttpActionResult Contract()
        {
            var user = new M_User();
            if (isAuthorized(out user))
            {
                var data = M_Contract.API.GetAll();

                return Json(new
                {
                    code = HttpStatusCode.OK,
                    data,
                });
            }

            return Content(HttpStatusCode.Forbidden, new
            {
                code = HttpStatusCode.Forbidden,
                status = "error",
                message = "Bearer token not valid"
            });

        }


    }
}
