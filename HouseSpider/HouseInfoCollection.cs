using System;
using System.Collections.Generic;
using System.Linq;
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
            PriceDetail price_detail = new PriceDetail();
            try
            {
                IWebDriver driver = new ChromeDriver("C:\\Program Files (x86)\\Google\\Chrome\\Application");
                driver.Url = HouseLink;
                driver.Manage().Window.Maximize();
                Thread.Sleep(2000);
                var html = new HtmlDocument();
                html.LoadHtml(driver.PageSource);
                driver.Close();
                var root = html.DocumentNode;
                houseInfo.description = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("listingoverview"))
                    .ToList()[0]
                    .Descendants("section")
                    .ToList()[0].InnerText.Trim();

                price_detail.list_price = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[0].Descendants("td")
                    .ToList()[0].InnerText.Trim();
                price_detail.gross_taxes = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[0].Descendants("td")
                    .ToList()[1].InnerText.Trim();
                price_detail.strata_maintenance_fees = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[0].Descendants("td")
                    .ToList()[2].InnerText.Trim();
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
