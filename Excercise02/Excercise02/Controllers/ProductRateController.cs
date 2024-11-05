using Excercise02.Contexts;
using Excercise02.DAL;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.Controllers
{
    public class ProductRateController : Controller
    {
        private static ProductRate_DALBase productRate_DALBase = new ProductRate_DALBase();

        #region Get All the product rates
        [Route("ProductRate")]
        [HttpGet]
        public async Task<IActionResult> Index(string productName, DateTime? priceAppliedDate)
        {
            List<ProductRateModel> list = await productRate_DALBase.GetAllProductRates();

            #region Searching
            if (!string.IsNullOrEmpty(productName))
            {
                list = list.Where(pr => pr.Product.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (priceAppliedDate.HasValue)
            {
                list = list.Where(pr => pr.PriceAppliedDate.Value.Date == priceAppliedDate.Value.Date).ToList();
            }
            #endregion

            ViewBag.ProductName = productName;
            ViewBag.PriceAppliedDate = priceAppliedDate?.ToString("yyyy-MM-dd");

            return View(list);
        }
        #endregion

    }
}
