using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TestEx2
{
    public class Temperature
    {
        public double temp;
        public double humidity;
    }
    public class Sun
    {
        public double sunrise;
        public double sunset;
    }
    public class WeatherResponce
    {
        public Temperature main;
        public Sun sys;
        public string name;
        public double timezone;
    }
}
