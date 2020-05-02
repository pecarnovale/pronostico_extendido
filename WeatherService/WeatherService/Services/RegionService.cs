using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace WeatherService.Services
{
    public class RegionService
    {


        public JArray GetCountries()
        {

            var jsonString = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Json_Data/countries.json"));
            JObject jObj = JObject.Parse(jsonString);
            var result = jObj["countries"] .OrderBy(c => c["name"]).Select(c => c);
            return new JArray(result);
        }

        public JArray GetCities(int pais)
        {
            var jsonString = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Json_Data/states.json"));
            var jObj = JObject.Parse(jsonString);
            var result = jObj["states"].Where(c => (int)c["id_country"] == pais).OrderBy(c => c["name"]).Select(c => c);
            return new JArray(result);
        }

    }
}