using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Core/Models/WeatherData.cs
namespace Core.Models
{
    public class WeatherData
    {
        public string Location { get; set; }
        public float Temperature { get; set; }
        public float TempMax { get; set; }
        public float TempMin { get; set; }
        public float Humidity { get; set; }
        public float PrecipProb { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }

        // Aquí agregamos la propiedad 'Description' (puede ser un string que describa el clima)
        public string Description { get; set; }
        public float Pressure { get; set; }
        public string Icon { get; set; }
    }
}

