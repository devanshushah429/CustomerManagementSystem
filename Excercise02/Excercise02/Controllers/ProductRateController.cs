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
        public async Task<IActionResult> Index()
        {
            List<ProductRateModel> list = await productRate_DALBase.GetAllProductRates();
            return View(list);
        }
        #endregion
    }
}
