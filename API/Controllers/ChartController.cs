using KMS.Logs.Model;
using KMS.Management.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KMS.API.Controllers
{
    public class ChartController : APIController
    {
        // GET api/<controller>
        [ActionName("hourly")]
        [HttpGet]
        public IHttpActionResult Hourly()
        {
            var user = new M_User();
            if (isAuthorized(out user))
            {
                var data = Vendor_Log.GetHourlyInterval(1 /*1 hour*/, DateTime.Now);
                //foreach(DataColumn col in data.Columns)
                //{
                //    col.ColumnName = col.ToString().ToUpper()
                //}

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


        [ActionName("daily")]
        [HttpGet]
        public IHttpActionResult Daily()
        {
            var user = new M_User();
            if (isAuthorized(out user))
            {
                var data = Vendor_Log.GetDailyInterval(1 /*1 day*/, DateTime.Now, null, null, null);

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

        [ActionName("monthly")]
        [HttpGet]
        public IHttpActionResult Monthly()
        {
            var user = new M_User();
            if (isAuthorized(out user))
            {
                var data = Vendor_Log.GetMonthlyInterval(DateTime.Now);

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


        [ActionName("summary")]
        [HttpGet]
        public IHttpActionResult Summary()
        {
            var user = new M_User();
            if (isAuthorized(out user))
            {
                var data = Vendor_Log.GetSummary(0);

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

        [ActionName("daily_counter")]
        [HttpGet]
        public IHttpActionResult DailyCounter()
        {
            var user = new M_User();
            if (isAuthorized(out user))
            {
                var data = Vendor_Log.GetDailySummary(0);

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