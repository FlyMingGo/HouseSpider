using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using HouseSpider.Model;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HouseSpider
{
    class HouseInfoCollection
    {
        public static HouseInfo GetHouseInfo(string HouseLink)
        {
            HouseInfo houseInfo = new HouseInfo();
            try
            {
                IWebDriver driver = new ChromeDriver("C:\\Program Files (x86)\\Google\\Chrome\\Application");
                driver.Url = HouseLink;
                driver.Manage().Window.Maximize();
                Thread.Sleep(2000);
                var html = new HtmlDocument();
                html.LoadHtml(driver.PageSource);
                driver.Close();
                return houseInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return houseInfo;
            }
        }
    }
}
