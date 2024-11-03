using Excercise02.Models;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.DAL
{
    public class Product_DALBase : DAL_Helper
    {

        #region Get All Products
        public async Task<List<ProductModel>> GetAllProducts()
        {
            DateTime today = DateTime.Now;

            List<ProductModel> products = await _context.Products
                .GroupJoin(
                    _context.ProductRate
                        .Where(productRate => productRate.PriceAppliedDate <= today),
                    product => product.ProductID,
                    productRate => productRate.ProductID,
                    (product, productRate) => new
                    {
                        Product = product,
                        LatestRate = productRate
                            .OrderByDescending(productRate => productRate.PriceAppliedDate)
                            .FirstOrDefault()
                    })
                .Select(productRate => new ProductModel
                {
                    ProductID = productRate.Product.ProductID,
                    ProductName = productRate.Product.ProductName,
                    ProductPrice = productRate.LatestRate != null ? productRate.LatestRate.ProductRate : 0,
                    Description = productRate.Product.Description,
                    Created = productRate.Product.Created,
                    Modified = productRate.Product.Modified
                })
                .ToListAsync();
            return products;
        }
        #endregion

        #region Get Detail of Specific Product
        public async Task<ProductModel> GetProductDetails(int? productID)
        {
            ProductModel? productModel = await _context.Products
            .Where(product => product.ProductID == productID)
            .GroupJoin(
                _context.ProductRate
                    .Where(pr => pr.PriceAppliedDate <= DateTime.Now),
                product => product.ProductID,
                productRate => productRate.ProductID,
                (product, productRates) => new
                {
                    Product = product,
                    LatestRate = productRates
                        .OrderByDescending(pr => pr.PriceAppliedDate)
                        .FirstOrDefault()
                })
            .Select(pr => new ProductModel
            {
                ProductID = pr.Product.ProductID,
                ProductName = pr.Product.ProductName,
                ProductPrice = pr.LatestRate != null ? pr.LatestRate.ProductRate : 0,
                Description = pr.Product.Description,
                Created = pr.Product.Created,
                Modified = pr.Product.Modified
            })
            .FirstOrDefaultAsync();
            return productModel;
        }
        #endregion

        #region Add the Product
        public async Task AddProduct(ProductModel product)
        {
            product.Created = DateTime.Now;
            product.Modified = DateTime.Now;

            _context.Products.Add(product);
            await SaveChangesAsync();

            // Add the rate in product rate table
            ProductRate_DALBase productRate_DALBase = new ProductRate_DALBase();
            productRate_DALBase.AddProductRate(product);
        }
        #endregion

        #region Edit The Product
        public async Task EditProduct(ProductModel product)
        {
            ProductModel existingProduct = await GetProductDetails(product.ProductID);
            if (existingProduct != null)
            {
                // Add the rate in product rate table
                if (existingProduct.ProductPrice != product.ProductPrice)
                {
                    ProductRate_DALBase productRate_DALBase = new ProductRate_DALBase();
                    productRate_DALBase.AddProductRate(product);
                }

                existingProduct.ProductName = product.ProductName;
                existingProduct.Description = product.Description;
                existingProduct.Modified = DateTime.Now;

                _context.Products.Update(existingProduct);
                await SaveChangesAsync();
            }
        }
        #endregion

        #region Delete the Product
        public async Task DeleteProduct(int id)
        {
            ProductModel? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
