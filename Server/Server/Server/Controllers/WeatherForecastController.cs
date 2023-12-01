using Microsoft.AspNetCore.Mvc;
using Server.Demo;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly Starter _starter;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, Starter starter)
    {
        _logger = logger;
        _starter = starter;
    }

    [HttpPost]
    public ClimateData Upsert(ClimateData user)
    {
        _starter.Add(user);
        return user;
    }
    [HttpGet]
    [Route("GetAll")]
    public Task<IReadOnlyCollection<ClimateData>> GetUsers()
    {
        return _starter.GetAll();
    }
    [HttpGet]
    [Route("GetLatest")]
    public Task<ClimateData?> GetLatest()
    {
        return _starter.GetLatest();
    }
}

