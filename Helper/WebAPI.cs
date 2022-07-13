using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace KMS.Helper
{
    public static class WebAPI
    {
        public static HttpStatusCode getToken(this HttpRequestMessage requestMessage)
        {
            IEnumerable<string> headerValues;
            if (requestMessage.Headers.TryGetValues("token", out headerValues))
            {
                if (headerValues.FirstOrDefault() == "123456")
                {
                    return HttpStatusCode.OK;
                }
            }
            return HttpStatusCode.BadRequest;
        }
    }
}