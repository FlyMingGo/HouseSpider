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
            Info info = new Info();
            HomeFacts homeFacts = new HomeFacts();
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
                var price_detail_items = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[0].Descendants("tr")
                    .ToList();
                foreach (var item in price_detail_items)
                {
                    if(item.Descendants("th").ToList()[0].InnerText.Contains("List Price"))
                    {
                        price_detail.list_price = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Gross Taxes"))
                    {
                        price_detail.gross_taxes = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Strata Maintenance Fees"))
                    {
                        price_detail.strata_maintenance_fees = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                }
                var homeFacts_items = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[1].Descendants("tr")
                    .ToList();
                foreach (var item in homeFacts_items)
                {
                    if (item.Descendants("th").ToList()[0].InnerText.Contains("Bedrooms"))
                    {
                        homeFacts.bedrooms = Convert.ToInt32(item.Descendants("td").ToList()[0].InnerText.Trim());
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Bathrooms"))
                    {
                        homeFacts.bathrooms = Convert.ToInt32(item.Descendants("td").ToList()[0].InnerText.Trim());
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Property Type"))
                    {
                        homeFacts.property_type = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Lot Size"))
                    {
                        homeFacts.lot_size = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Depth"))
                    {
                        homeFacts.depth = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Frontage"))
                    {
                        homeFacts.property_type = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Year Built"))
                    {
                        homeFacts.year_built = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Title"))
                    {
                        homeFacts.title = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Style"))
                    {
                        homeFacts.style = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Features"))
                    {
                        homeFacts.features = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Amenities"))
                    {
                        homeFacts.amenities = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                    else if (item.Descendants("th").ToList()[0].InnerText.Contains("Appliances"))
                    {
                        homeFacts.appliances = item.Descendants("td").ToList()[0].InnerText.Trim();
                    }
                }
                homeFacts.bedrooms = Convert.ToInt32(root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[1].Descendants("td")
                    .ToList()[0].InnerText.Trim());
                homeFacts.bathrooms = Convert.ToInt32(root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[1].Descendants("td")
                    .ToList()[1].InnerText.Trim());
                homeFacts.property_type = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[1].Descendants("td")
                    .ToList()[2].InnerText.Trim();
                homeFacts.year_built = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[1].Descendants("td")
                    .ToList()[3].InnerText.Trim();
                homeFacts.title = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[1].Descendants("td")
                    .ToList()[4].InnerText.Trim();
                homeFacts.style = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[1].Descendants("td")
                    .ToList()[5].InnerText.Trim();
                homeFacts.community = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "")
                    .Contains("sectionblock-display"))
                    .ToList()[0]
                    .Descendants("tbody").ToList()[1].Descendants("td")
                    .ToList()[6].InnerText.Trim();
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
