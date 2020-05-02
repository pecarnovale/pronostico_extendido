using WeatherService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WeatherService.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            try
            {
                if (login == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);

                //TODO: Validate credentials Correctly, this code is only for demo !!
                bool isCredentialValid = (login.Username == "user") && (login.Password == "123456");
                if (isCredentialValid)
                {
                    var token = TokenGenerator.GenerateTokenJwt(login.Username);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch(Exception e)
            {
                return Conflict();
            }
        }
    }
}
