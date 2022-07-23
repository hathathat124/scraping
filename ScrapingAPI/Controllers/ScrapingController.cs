using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using Scraping;
using Scraping.Interfaces;
using Scraping.Models;
using Scraping.Services;
using static Scraping.Models.Common;

namespace ScrapingAPI.Controllers
{
    [ApiController]

    public class ScrapingController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<ScrapingController> _logger;
        private readonly IScrapingService _scrapingService;


        public ScrapingController(ILogger<ScrapingController> logger, IScrapingService scrapingService)
        {
            _scrapingService = scrapingService;
            _logger = logger;
        }

        [HttpGet]
        [Route("scraping/v1/makroscraping")]
        public async Task<MakroDataModel> MakroScraping()
        {
            MakroDataModel dataResponse = new MakroDataModel();
            try
            {
                System.Console.WriteLine("API!");
                var dataRequest = new List<string>() { "น้ำอัดลม" };
                dataResponse = await _scrapingService.Makro(dataRequest);

                return dataResponse;
            }
            catch (Exception ex)
            {
                dataResponse.status = new Status
                {
                    code = "500",
                    message = ex.Message
                };
                return dataResponse;
            };
        }

        [HttpGet]
        [Route("scraping/v1/alive")]
        public string Alive()
        {
            return "Success";
        }

        [HttpGet]
        [Route("scraping/v1/shoppeescraping")]
        public async Task<MakroDataModel> ShoppeeScraping()
        {
            var dataRequest = new List<string>() { "����Ѵ��" };
            var dataResponse = await _scrapingService.Makro(dataRequest);

            return dataResponse;
        }

    }
}