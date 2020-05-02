using WeatherService.Models;
using WeatherService.Services;
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
    [RoutePrefix("api/region")]
    [Authorize]
    public class RegionController : ApiController
    {

        [HttpPost]
        public async Task<IHttpActionResult> GetCountries()
        {
            try
            {
                RegionService r = new RegionService();
                return Ok(r.GetCountries());
            }
            catch (Exception e)
            {
                return Conflict();
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetCities(RegionRequest rr)
        {
            try
            {
                RegionService r = new RegionService();
                return Ok(r.GetCities(rr.StateId));
            }
            catch (Exception e)
            {
                return Conflict();
            }
        }
    }
}
