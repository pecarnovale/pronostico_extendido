using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;

namespace WeatherService.Services
{
    public class WeatherService
    {


        public async Task<JObject> GetWeather(string country,string state)
        {
            JObject returnValue = default(JObject);

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(String.Format("https://api.opencagedata.com/geocode/v1/json?q={0},{1}&key={2}&limit=1", country,state, ConfigurationManager.AppSettings["GEO_SERVICE_KEY"]));
                    response.EnsureSuccessStatusCode();

                    var result = JObject.Parse(((HttpResponseMessage)response).Content.ReadAsStringAsync().Result.ToString());
                    var lat = result["results"][0]["geometry"]["lat"].ToString();
                    var lon = result["results"][0]["geometry"]["lng"].ToString();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = await client.GetAsync(String.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&appid={2}&units=metric&lang=sp", lat,lon, ConfigurationManager.AppSettings["WEATHER_SERVICE_KEY"])); 
                    response.EnsureSuccessStatusCode();
                    result = JObject.Parse(((HttpResponseMessage)response).Content.ReadAsStringAsync().Result.ToString());
                    returnValue = result;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
       
}