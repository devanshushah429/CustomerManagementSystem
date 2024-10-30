using Excercise02.Contexts;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Excercise02.Controllers
{
    public class PartyWiseProductController : Controller
    {
        private readonly AppDbContext _context;

        public PartyWiseProductController(AppDbContext context)
        {
            _context = context;
        }


        #region Get the products as per party
        [HttpGet]
        public async Task<List<PartyWiseProductModel>> GetProductsForParty(int id)
        {
            List<PartyWiseProductModel> list = await _context.PartyWiseProduct.Where(p => p.PartyID == id)
                .Join(_context.Products, partyWiseProduct => partyWiseProduct.ProductID, product => product.ProductID, (partyWiseProduct, product) => new PartyWiseProductModel() { Product = product }).ToListAsync();
            return list;
        }
        #endregion

        public async Task<IActionResult> AddPartyWiseProduct(int id)
        {
            List<int?> excludedProductIds = await _context.PartyWiseProduct
                .Where(partyWiseProduct => partyWiseProduct.PartyID == id)
                .Select(partyWiseProduct => partyWiseProduct.ProductID)
                .ToListAsync();

            List<ProductModel> availableProducts = await _context.Products
                .Where(product => !excludedProductIds.Contains(product.ProductID))
                .ToListAsync();
            ViewBag.PartyID = id;

            return View(availableProducts);
        }

        [HttpPost]
        public IActionResult AddPartyWiseProduct(int partyID, int productID)
        {
            _context.PartyWiseProduct.Add(new PartyWiseProductModel { ProductID = productID, PartyID = partyID });
            _context.SaveChanges();
            return RedirectToAction("Details", "Party", new { id = partyID });
        }
    }
}
