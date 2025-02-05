using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ApiResponse
    {
        public List<WeatherApiResponse> Days { get; set; }
        public string Description { get; set; }
        public string ResolvedAddress { get; set; }
    }
}
