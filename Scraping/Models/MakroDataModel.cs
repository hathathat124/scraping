using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping.Models
{
    public class MakroDataModel : BaseResponse
    {
        public List<ProductDetail> data { get; set; }

        public class ProductDetail
        {
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string ProductPerPrice { get; set; }
            public string Price { get; set; }
            public string GetAll => $"ProductName : {this.ProductName} {Environment.NewLine}" +
                    $"Price : {this.Price} {Environment.NewLine}" +
                    $"ProductPerPrice : {this.ProductPerPrice} {Environment.NewLine}";
            public override string ToString()
            {
                string str =
                    $"ProductName : {this.ProductName} {Environment.NewLine}" +
                    $"Price : {this.Price} {Environment.NewLine}";
                if (!string.IsNullOrEmpty(this.ProductPerPrice))
                {
                    str += $"ProductPerPrice : {this.ProductPerPrice} {Environment.NewLine}";
                }

                return str;
            }
        }
    }
}
