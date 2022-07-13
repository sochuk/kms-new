using KMS.Helper;
using KMS.Management.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace KMS.API.Controllers
{
    public class APIController : ApiController
    {
        public bool isAuthorized(out M_User user)
        {
            user = null;
            string token = null;
            if (Request.Headers.Contains("Authorization"))
            {
                token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            }

            if (Request.Headers.Contains("token"))
            {
                token = Request.Headers.GetValues("token").FirstOrDefault();
            }

            if (token == null)
            {
                return false;
            }
            else
            {
                token = token.Replace("Bearer", "").Trim();
                var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["key"].ToString());
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                try
                {
                    SecurityToken validationsResult;
                    var result = tokenHandler.ValidateToken(token, tokenValidationParameters, out validationsResult);
                    if (result != null)
                    {
                        var id = result.Claims.FirstOrDefault(x => x.Type.Equals(UserClaim.User_Id)).Value.ToInteger();
                        user = M_User.Get(id);
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    return false;
                }
            }

            return false;
        }


    }
}