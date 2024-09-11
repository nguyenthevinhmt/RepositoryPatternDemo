using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolePermissionDemo.Shared.ApplicationBase.Common;
using RolePermissionDemo.Shared.Consts.Permissions;
using RolePermissionDemo.Shared.Filters;

namespace RolePermissionDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpContextAccessor _httpContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _httpContext = httpContext; 
        }

        [PermissionFilter(
            PermissionKeys.MenuUserAccount,
            PermissionKeys.MenuDashboard
        )]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var userId = _httpContext.GetCurrentUserId();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
