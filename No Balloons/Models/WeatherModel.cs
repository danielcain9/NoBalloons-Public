﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace No_Balloons.Models
{
   
    public class ResultViewModel
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Description { get; set; }
        public string Humidity { get; set; }
        public string TempFeelsLike { get; set; }
        public string Temp { get; set; }
        public string TempMax { get; set; }
        public string TempMin { get; set; }
        public string WeatherIcon { get; set; }
    }

    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public double id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public double deg { get; set; }
    }

    public class Clouds
    {
        public double all { get; set; }
    }

    public class Sys
    {
        public double type { get; set; }
        public double id { get; set; }
        public string country { get; set; }
        public double sunrise { get; set; }
        public double sunset { get; set; }
    }

    public class RootObject
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public double visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public double dt { get; set; }
        public Sys sys { get; set; }
        public double timezone { get; set; }
        public double id { get; set; }
        public string name { get; set; }
        public double cod { get; set; }
    }
}