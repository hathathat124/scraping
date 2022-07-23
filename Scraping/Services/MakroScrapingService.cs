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
        private bool isNextPage = true;
        private int currentPage = 1;
        public ScrapingService(IMakroScrapingProcess makroScrapingProcess, IShoppeeScrapingProcess shoppeeScrapingProcess)
        {
            _makroScrapingProcess = makroScrapingProcess;
            _shoppeeScrapingProcess = shoppeeScrapingProcess;
        }

        public async Task<MakroDataModel> Makro(List<string> keywords)
        {
            MakroDataModel dataModel = new MakroDataModel();
            try
            {
                System.Console.WriteLine("Lib!");

                using var webDriver = _makroScrapingProcess.SettingWebDriver();


                foreach (var item in keywords)
                {
                    await _makroScrapingProcess.InputKeyword(item);
                    while (isNextPage)
                    {
                        var dataElement = await _makroScrapingProcess.FindElement();
                        var dataListofPage = await _makroScrapingProcess.RetrieveData(dataElement);

                        dataModel.data.AddRange(dataListofPage);
                        isNextPage = _makroScrapingProcess.NextPage(ref currentPage);
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return dataModel;
        }

        public async Task<ShoppeeDataModel> Shoppee(List<string> keywords)
        {
            ShoppeeDataModel dataModel = new ShoppeeDataModel();
            try
            {
                using var webDriver = new ChromeDriver();
                _shoppeeScrapingProcess.SettingWebDriver(webDriver);

                foreach (var item in keywords)
                {
                    await _shoppeeScrapingProcess.InputKeyword(item);
                    while (isNextPage)
                    {
                        var dataElement = await _shoppeeScrapingProcess.FindElement();
                        var dataListofPage = await _shoppeeScrapingProcess.RetrieveData(dataElement);

                        dataModel.data.AddRange(dataListofPage);
                        isNextPage = _shoppeeScrapingProcess.NextPage(ref currentPage);
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return dataModel;
        }


    }
}
