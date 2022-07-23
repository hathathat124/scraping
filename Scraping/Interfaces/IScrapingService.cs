using Scraping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping.Interfaces
{
    public interface IScrapingService
    {
        Task<MakroDataModel> Makro(List<string> keywords);
        Task<ShoppeeDataModel> Shoppee(List<string> keywords);

    }
}
