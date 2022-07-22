using Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using Scraping.Interfaces.IProcess;
using Scraping.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Scraping.Models.Common;
using static Scraping.Models.MakroDataModel;

namespace Scraping.Process
{
    public class MakroScrapingProcess : IMakroScrapingProcess
    {
        private IWebDriver _webDriver;
        public void SettingWebDriver(ChromeDriver webDriver)
        {            
            webDriver.Manage().Window.Size = new Size(1920, 1080);
            _webDriver = webDriver;
        }

        public async Task InputKeyword(string keyword)
        {
            _webDriver.Navigate().GoToUrl(AppUrl.UrlMakro);

            string xpathInput = "/html/body/div[1]/div/div/div[1]/div[1]/div/div[2]/div/div/div[2]/div[1]/div/div/div[1]/div/div[1]/div/input";
            //ChromeDriver driver = new ChromeDriver();

            var input = _webDriver.FindElement(By.XPath(xpathInput));
            foreach (var item in keyword)
            {
                await Task.Delay(200);
                input.SendKeys(item.ToString());
            }
            input.SendKeys(Keys.Enter);
        }

        public async Task<ReadOnlyCollection<IWebElement>> FindElement()
        {       
            await _webDriver.ScrollPage();

            string xpathgetvalue = "//*[@id=\"scrollPaginatorTop\"]/div[2]/div/div";
            
            var data = _webDriver.FindElements(By.XPath(xpathgetvalue));
            return data;
        }

        public async Task<List<ProductDetail>> RetrieveData(ReadOnlyCollection<IWebElement> dataelement)
        {
            List<ProductDetail> dataList = new List<ProductDetail>();
            dataList =  dataelement.Select(item => new ProductDetail
                {
                    ProductName = item.FindElement(By.XPath("div/div/div/a/div")).Text,// name product
                    ProductCode = item.FindElement(By.XPath("div/div/div/div[1]")).Text, // price              
                    Price = item.FindElement(By.XPath("div/div/div/div[2]/div[1]/div[2]")).Text, // price              
                    ProductPerPrice = item.FindElement(By.XPath("div/div/div/div[3]/div")).Text, // price       
                }).ToList();
            return dataList;
        }

        public bool NextPage(ref int currentPage)
        {
            try
            {
                currentPage += 1;
                var pageToGo = currentPage;
                var classPageBTN = "//*[@class=\"pagination  px-1 mx-1 px-lg-2 py-lg-1 mx-1\"]";
                var pageElement = _webDriver.FindElements(By.XPath(classPageBTN));
          

                var pageClickElement = pageElement.Where(w => Convert.ToInt16(w.Text) == pageToGo).First();

                pageClickElement.Click();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        
    }
}
