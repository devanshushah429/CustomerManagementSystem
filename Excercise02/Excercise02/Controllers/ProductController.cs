using Excercise02.Contexts;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        #region List all the products
        [Route("Product")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var today = DateTime.Now;

            List<ProductModel> products = await _context.Products
                .GroupJoin(
                    _context.ProductRate
                        .Where(pr => pr.PriceAppliedDate <= today),
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
                .ToListAsync();

            return View(products);
        }
        #endregion

        #region Show Details of Product
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProductModel productModel = await _context.Products
            .Where(product => product.ProductID == id)
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
                ProductPrice = pr.LatestRate != null ? pr.LatestRate.ProductRate : 0, // Default to 0 if no rate
                Description = pr.Product.Description,
                Created = pr.Product.Created,
                Modified = pr.Product.Modified
            })
            .FirstOrDefaultAsync();
            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }
        #endregion

        #region Show Form to create new Product or Edit the Product of given id
        [HttpGet]
        public async Task<IActionResult> AddEditProduct(int? id)
        {
            ProductModel? productModel = new ProductModel();
            if (id != null)
            {
                productModel = await _context.Products
            .Where(product => product.ProductID == id)
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
                ProductPrice = pr.LatestRate != null ? pr.LatestRate.ProductRate : 0, // Default to 0 if no rate
                Description = pr.Product.Description,
                Created = pr.Product.Created,
                Modified = pr.Product.Modified
            })
            .FirstOrDefaultAsync();
            }
            return View("AddEditPage", productModel);
        }
        #endregion

        #region Add the new product or edit
        [HttpPost]
        public async Task<IActionResult> AddEditProduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == null)
                {
                    // Case 1: Add new product
                    product.Created = DateTime.Now;
                    product.Modified = DateTime.Now;

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync(); // Save to get the generated ProductID

                    // Now that we have the ProductID, we can add to ProductRate
                    _context.ProductRate.Add(new ProductRateModel()
                    {
                        ProductID = product.ProductID, // Use the ID from the newly created product
                        ProductRate = product.ProductPrice,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        PriceAppliedDate = DateTime.Now
                    });
                }
                else
                {
                    // Case 2: Update existing product
                    var existingProduct = await _context.Products.FindAsync(product.ProductID);
                    if (existingProduct != null)
                    {
                        // Update properties
                        existingProduct.ProductName = product.ProductName;
                        existingProduct.Description = product.Description;
                        existingProduct.Modified = DateTime.Now; // Only update modified timestamp

                        _context.Products.Update(existingProduct);
                        await _context.SaveChangesAsync(); // Save changes before adding ProductRate

                        // Add new ProductRate record for the updated product
                        _context.ProductRate.Add(new ProductRateModel()
                        {
                            ProductID = existingProduct.ProductID, // Use existing product's ID
                            ProductRate = product.ProductPrice,
                            PriceAppliedDate = DateTime.Now,
                            Created = DateTime.Now,
                            Modified = DateTime.Now
                        });
                    }
                    else
                    {
                        // Case 3: Product does not exist
                        return NotFound();
                    }
                }

                // Final save for ProductRate
                try
                {
                    await _context.SaveChangesAsync(); // This will save both product and product rate changes
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the product: " + ex.Message);
                    return View(product);
                }

                return RedirectToAction("Index");
            }
            return View(product);
        }
        #endregion

        #region Open the Confirm Delete Page
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        #endregion

        #region Handle POST request to delete a product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
