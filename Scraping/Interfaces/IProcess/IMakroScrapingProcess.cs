using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scraping.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Scraping.Models.MakroDataModel;

namespace Scraping.Interfaces.IProcess
{
    public interface IMakroScrapingProcess
    {
        IWebDriver SettingWebDriver();
        Task<ReadOnlyCollection<IWebElement>> FindElement();
        Task InputKeyword(string keyword);
        Task<List<ProductDetail>> RetrieveData(ReadOnlyCollection<IWebElement> dataelement);
        bool NextPage(ref int currentPage);
    }
}
