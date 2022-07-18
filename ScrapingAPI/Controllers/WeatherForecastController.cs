using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Chrome;
using Scraping;
using Scraping.Interfaces;
using Scraping.Models;
using Scraping.Services;
using static Scraping.Models.Common;

namespace ScrapingAPI.Controllers
{
    [ApiController]

    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IScrapingService _scrapingService;

        
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IScrapingService scrapingService)
        {
            _scrapingService = scrapingService;
            _logger = logger;
        }

        [HttpGet]
        [Route("scraping/v1/makroscraping")]
        public async Task<MakroDataModel> MakroScraping()
        {
            var dataRequest = new List<string>() { "����Ѵ��" };
            var dataResponse = await _scrapingService.Makro(dataRequest);

            return dataResponse;
        }
        [HttpGet]
        [Route("scraping/v1/alive")]
        public string Alive()
        {
            return "Success";
        }
    }
}