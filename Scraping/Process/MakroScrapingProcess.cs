using Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using Scraping.Interfaces;
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
    public class MakroScrapingProcess : IScrapingProcess
    {
        private IWebDriver _webDriver;
        public void SettingWebDriver(ChromeDriver webDriver)
        {            
            webDriver.Manage().Window.Size = new Size(1920, 1080);
            _webDriver = webDriver;
        }

        public async Task<ReadOnlyCollection<IWebElement>> FindElement(string keyword)
        {
            _webDriver.Navigate().GoToUrl(AppUrl.UrlMakro);

            string xpath = "/html/body/div[1]/div/div/div[1]/div[1]/div/div[2]/div/div/div[2]/div[1]/div/div/div[1]/div/div[1]/div/input";
            //ChromeDriver driver = new ChromeDriver();

            var input = _webDriver.FindElement(By.XPath(xpath));
            foreach (var item in keyword)
            {
                await Task.Delay(200);
                input.SendKeys(item.ToString());
            }
            input.SendKeys(Keys.Enter);

            await _webDriver.ScrollPage();

            //string xpathgetvalue = "//*[@id=\"scrollPaginatorTop\"]/div[2]/div/div[1]/div/div/div/a/div";
            string xpathgetvalue = "//*[@id=\"scrollPaginatorTop\"]/div[2]/div/div";
            //string xpathgetvalue = "/html/body/div[1]/div/div[3]/div/div[2]/div[3]/div[2]/div[1]/a/div/div";

            var data = _webDriver.FindElements(By.XPath(xpathgetvalue));
            return data;
        }

        public async Task<MakroDataModel> RetrieveData(ReadOnlyCollection<IWebElement> dataelement, MakroDataModel datamodel)
        {
            //List<MakroDataModel> data = null;
            ProductDetail datarow;
       datamodel.data = dataelement.Select(item => new ProductDetail
            {
                ProductName = item.FindElement(By.XPath("div/div/div/a/div")).Text,// name product
                ProductCode = item.FindElement(By.XPath("div/div/div/div[1]")).Text, // price              
                Price = item.FindElement(By.XPath("div/div/div/div[2]/div[1]/div[2]")).Text, // price              
                ProductPerPrice = item.FindElement(By.XPath("div/div/div/div[3]/div")).Text, // price       
            }).ToList();

            //datamodel.Add(new ProductDetail
            //{                    
            //    ProductName = item.FindElement(By.ClassName("UjjMrh")).Text,// name product
            //    Price = item.FindElement(By.ClassName("_1heB4J")).Text, // price                    
            //    //ProductPerPrice = item.FindElement(By.ClassName("div/div/div/div[3]/div")).Text // price per item
            //});
       
            return datamodel;

        }

    }
}
