using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.DataModels;
using Server.Demo;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Starter _starter;

        public PredictionController(ILogger<WeatherForecastController> logger, Starter starter)
        {
            _logger = logger;
            _starter = starter;
        }
        [HttpPost]
        public Prediction Upsert([FromBody] Prediction user)
        {
            _starter.Add(user);
            return user;
        }

        [HttpGet]
        [Route("GetLatest")]
        public Task<Prediction?> GetLatest()
        {
            return _starter.GetLatestPrediction();
        }
    }
}

