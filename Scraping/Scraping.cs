using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using Scraping.Interfaces;
using Scraping.Models;
using Scraping.Services;
using static Scraping.Models.Common;

namespace Scraping
{
    public class ScrapingSite
    {


        private readonly IScrapingService _scrapingService;

        public ScrapingSite(IScrapingService scrapingService)
        {
            _scrapingService = scrapingService;
        }

        public async Task<MakroDataModel> GetMakroData(List<string> keywords)
        {
            MakroDataModel data = new MakroDataModel();
            try
            {
                data = await _scrapingService.Makro(keywords);
            }
            catch (Exception ex)
            {
                data.status.code = "500";
                data.status.message = ex.Message;
            }
            return data;
        }

        public async Task<object> ShoppeeScraping(List<string> keywords)
        {
            ShoppeeDataModel data = new ShoppeeDataModel();
            try
            {
                data = await _scrapingService.Shoppee(keywords);
            }
            catch (Exception ex)
            {
                data.status.code = "500";
                data.status.message = ex.Message;
            }
            return new { success = true };
        }


    }
}