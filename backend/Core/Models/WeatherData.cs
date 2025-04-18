namespace Core.Models;

public class WeatherData
{
    public string Datetime { get; set; }
    public string Location { get; set; }
    public float Temperature { get; set; }
    public float TempMax { get; set; }
    public float TempMin { get; set; }
    public float Humidity { get; set; }
    public float PrecipProb { get; set; }
    public string Sunrise { get; set; }
    public string Sunset { get; set; }
    public string Description { get; set; }
    public float Pressure { get; set; }
    public string Icon { get; set; }
}
