using Excercise02.Contexts;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.Controllers
{
    public class ProductRateController : Controller
    {
        private readonly AppDbContext _context;
        public ProductRateController(AppDbContext context)
        {
            _context = context;
        }

        [Route("ProductRate")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ProductRateModel> list = await _context.ProductRate.Join(_context.Products,
                productRate => productRate.ProductID,
                product => product.ProductID,
                (productRate, product) => new ProductRateModel() { 
                    ProductRateID = productRate.ProductRateID, 
                    PriceAppliedDate = productRate.PriceAppliedDate, 
                    Product = product,
                    Created = productRate.Created, 
                    Modified = productRate.Modified,
                    ProductID = productRate.ProductID,
                    ProductRate = productRate.ProductRate
                }).ToListAsync();
            return View(list);
        }
    }
}
