using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherService.Models
{
    public class WeatherRequest
    {
        public string Country { get; set; }
        public string State { get; set; }
    }
}