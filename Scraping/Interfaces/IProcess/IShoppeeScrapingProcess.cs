using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scraping.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping.Interfaces.IProcess
{
    public interface IShoppeeScrapingProcess
    {
        void SettingWebDriver(ChromeDriver webDriver);
        Task<ReadOnlyCollection<IWebElement>> FindElement(string keyword);
        Task<MakroDataModel> RetrieveData(ReadOnlyCollection<IWebElement> dataelement, MakroDataModel datamodel);        
    }
}
