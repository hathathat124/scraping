using Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scraping.Interfaces;
using Scraping.Models;
using Scraping.Process;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Scraping.Models.Common;

namespace Scraping.Services
{
    public class MakroScrapingService : IScrapingService
    {
        private readonly IScrapingProcess _scrapingProcess;

        public MakroScrapingService(IScrapingProcess scrapingprocess)
        {
            _scrapingProcess = scrapingprocess;
        }

        public async Task<MakroDataModel> Makro(List<string> keywords)
        {
            MakroDataModel dataModel = new MakroDataModel();
            try
            {
                using var webDriver = new ChromeDriver();
                _scrapingProcess.SettingWebDriver(webDriver);

                foreach (var item in keywords)
                {
                    var dataElement = await _scrapingProcess.FindElement(item);
                    dataModel = await _scrapingProcess.RetrieveData(dataElement, dataModel);
                }

            }
            catch (Exception ex) { 
            }
            return dataModel;
        }

        public Task<MakroDataModel> Shoppee(List<string> keywords)
        {
            throw new NotImplementedException();
        }
    }    
}
