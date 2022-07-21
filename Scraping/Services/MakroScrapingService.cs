using Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scraping.Interfaces;
using Scraping.Interfaces.IProcess;
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
    public class ScrapingService : IScrapingService
    {
        private readonly IMakroScrapingProcess _makroScrapingProcess;
        private readonly IShoppeeScrapingProcess _shoppeeScrapingProcess;

        public ScrapingService(IMakroScrapingProcess makroScrapingProcess , IShoppeeScrapingProcess shoppeeScrapingProcess)
        {
            _makroScrapingProcess = makroScrapingProcess;
            _shoppeeScrapingProcess = shoppeeScrapingProcess;
        }

        public async Task<MakroDataModel> Makro(List<string> keywords)
        {
            MakroDataModel dataModel = new MakroDataModel();
            try
            {
                using var webDriver = new ChromeDriver();
                _makroScrapingProcess.SettingWebDriver(webDriver);

                foreach (var item in keywords)
                {
                    var dataElement = await _makroScrapingProcess.FindElement(item);
                    dataModel = await _makroScrapingProcess.RetrieveData(dataElement, dataModel);
                }

            }
            catch (Exception ex) { 
            }
            return dataModel;
        }

        public async Task<MakroDataModel> Shoppee(List<string> keywords)
        {
            MakroDataModel dataModel = new MakroDataModel();
            try
            {
                using var webDriver = new ChromeDriver();
                _shoppeeScrapingProcess.SettingWebDriver(webDriver);

                foreach (var item in keywords)
                {
                    var dataElement = await _shoppeeScrapingProcess.FindElement(item);
                    dataModel = await _shoppeeScrapingProcess.RetrieveData(dataElement, dataModel);
                }

            }
            catch (Exception ex)
            {
            }
            return dataModel;
        }
    }    
}
