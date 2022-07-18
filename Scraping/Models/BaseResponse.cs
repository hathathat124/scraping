using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping.Models
{
    public class BaseResponse
    {
        public Status status { get; set; }        
}
    public class Status {
        public string code { get; set; }
        public string message { get; set; }
    }
}
