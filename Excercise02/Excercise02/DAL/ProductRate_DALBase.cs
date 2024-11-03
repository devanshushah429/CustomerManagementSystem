using Excercise02.Models;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.DAL
{
    public class ProductRate_DALBase:DAL_Helper
    {
        public async Task<List<ProductRateModel>> GetAllProductRates()
        {
            List<ProductRateModel> list = await _context.ProductRate.Join(_context.Products,
                productRate => productRate.ProductID,
                product => product.ProductID,
                (productRate, product) => new ProductRateModel()
                {
                    ProductRateID = productRate.ProductRateID,
                    PriceAppliedDate = productRate.PriceAppliedDate,
                    Product = product,
                    Created = productRate.Created,
                    Modified = productRate.Modified,
                    ProductID = productRate.ProductID,
                    ProductRate = productRate.ProductRate
                }).ToListAsync();
            return list;

        }
        public void AddProductRate(ProductModel product)
        {
            _context.ProductRate.Add(new ProductRateModel()
            {
                ProductID = product.ProductID,
                ProductRate = product.ProductPrice,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                PriceAppliedDate = DateTime.Now
            });
            SaveChanges();
        }
    }
}
