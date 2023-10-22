using Microsoft.AspNetCore.Mvc;
using Server.Demo;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    //private static readonly string[] Summaries = new[]
    //{
    //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly Starter _starter;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, Starter starter)
    {
        _logger = logger;
        _starter = starter;
    }

    //[HttpGet(Name = "GetWeatherForecast")]
    //public IEnumerable<WeatherForecast> Get()
    //{
    //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //    {
    //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}
    [HttpPost]
    public User Upsert(User user)
    {
        _starter.Add(user);
        return user;
    }
    [HttpGet]
    public Task<IReadOnlyCollection<User>> GetUsers()
    {
        return _starter.GetAll();
    }
}

