using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class Extensions
    {
        public static async Task ScrollPage(this IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            for (int i = 0; i < 5; i++)
            {
                js.ExecuteScript("scrollBy(0,750)");
                await Task.Delay(700);
            }
        }
    }

 
}
