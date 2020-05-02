using WeatherService.Models;
using WeatherService.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WeatherService.Controllers
{
    [RoutePrefix("api/weather")]
    [Authorize]
    public class WeatherController : ApiController
    {
        static HttpClient client = new HttpClient();

        [HttpPost]
        public async Task<IHttpActionResult> GetWeather([FromBody] WeatherRequest wr)
        {
            try
            {
                Services.WeatherService w = new Services.WeatherService();
                var result = await w.GetWeather(wr.Country, wr.State);
                return Ok(result);
            }
            catch(Exception e)
            {
                return Conflict();
            }
            
        }
    }
}
