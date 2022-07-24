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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using static Scraping.Models.Common;
using static Scraping.Models.MakroDataModel;

namespace Scraping.Process
{
    public class MakroScrapingProcess : IMakroScrapingProcess
    {
        private IWebDriver _webDriver;
        public IWebDriver SettingWebDriver()
        {

            string binEnvironment = Environment.GetEnvironmentVariable("GOOGLE_CHROME_BIN") ?? "";
            string chromeDriverEnvironment = Environment.GetEnvironmentVariable("CHROMEDRIVER_PATH") ?? "";
            //string binEnvironment = "/app/.apt/usr/bin/google-chrome";
            //string chromeDriverEnvironment = "/app/.chromedriver/bin/chromedriver";
            //string chromeDriverEnvironment = "";

            Console.WriteLine("chromeDriverEnvironment: " + chromeDriverEnvironment);
            Console.WriteLine("binEnvironment: " + binEnvironment);

            new DriverManager().SetUpDriver(new ChromeConfig());

            var setting = new ChromeOptions
            {
                //BinaryLocation = @"/app/.apt/usr/bin/google-chrome",
                BinaryLocation = binEnvironment
                //DebuggerAddress = "127.0.0.1:9222"
            };
            setting.AddArgument("--headless");
            setting.AddArgument("--disable-gpu");
            setting.AddArgument("--no-sandbox");
            setting.AddArgument("--disable-dev-shm-usage");
            Console.WriteLine("Setting Start");


            //_webDriver = new ChromeDriver(chromeDriverEnvironment, setting);
            _webDriver = new ChromeDriver(setting);
            _webDriver.Manage().Window.Size = new Size(1920, 1080);



            Console.WriteLine("new ChromeDriver Finish");            
            Console.WriteLine("Setting Finish");

            return _webDriver;
        }

        public async Task InputKeyword(string keyword)
        {
            Console.WriteLine("InputKeyword");
            Console.WriteLine("Html: "+ _webDriver.FindElement(By.ClassName("/html/body")));

            _webDriver.Navigate().GoToUrl(AppUrl.UrlMakro);
            Console.WriteLine("GoToUrl");
            string xpathInput = "/html/body/div[1]/div/div/div[1]/div[1]/div/div[2]/div/div/div[2]/div[1]/div/div/div[1]/div/div[1]/div/input";
            //ChromeDriver driver = new ChromeDriver();

            var input = _webDriver.FindElement(By.XPath(xpathInput));
            foreach (var item in keyword)
            {
                await Task.Delay(200);
                input.SendKeys(item.ToString());
            }
            input.SendKeys(Keys.Enter);
            Console.WriteLine("Enter search");
        }

        public async Task<ReadOnlyCollection<IWebElement>> FindElement()
        {
            await _webDriver.ScrollPage();
            Console.WriteLine("ScrollPage");
            string xpathGetValue = "//*[@id=\"scrollPaginatorTop\"]/div[2]/div/div";
            Console.WriteLine("Element value search");

            var data = _webDriver.FindElements(By.XPath(xpathGetValue));
            return data;
        }

        public async Task<List<ProductDetail>> RetrieveData(ReadOnlyCollection<IWebElement> dataelement)
        {
            List<ProductDetail> dataList = new List<ProductDetail>();
            Console.WriteLine("Section Retrievedata");

            dataList = dataelement.Select(item => new ProductDetail
            {
                ProductName = item.FindElement(By.XPath("div/div/div/a/div")).Text,// name product
                ProductCode = item.FindElement(By.XPath("div/div/div/div[1]")).Text, // price              
                Price = item.FindElement(By.XPath("div/div/div/div[2]/div[1]/div[2]")).Text, // price              
                ProductPerPrice = item.FindElement(By.XPath("div/div/div/div[3]/div")).Text, // price       
            }).ToList();
            Console.WriteLine("Section Retrievedata Success");
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
