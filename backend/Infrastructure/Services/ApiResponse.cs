namespace Infrastructure.Services;

public class ApiResponse
{
    public List<WeatherApiResponse> Days { get; set; }
    public string Description { get; set; }
    public string ResolvedAddress { get; set; }
}
