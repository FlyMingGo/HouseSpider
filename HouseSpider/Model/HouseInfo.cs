using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSpider.Model
{
    class HouseInfo
    {
        public string description { get; set; }
        public string address { get; set; }
        public string neighborhood { get; set; }
        public Info info { get; set; }
        public List<string> image { get; set; }
        public PriceDetail price_detail { get; set; }
        public HomeFacts home_facts { get; set; }
        public string mls_number { get; set; }
    }
}
