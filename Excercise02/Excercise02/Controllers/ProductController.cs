using Excercise02.Contexts;
using Excercise02.DAL;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.Controllers
{
    public class ProductController : Controller
    {
        private Product_DALBase _product_DALBase = new Product_DALBase();

        #region List all the products
        [Route("Product")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ProductModel> products = await _product_DALBase.GetAllProducts();
            return View(products);
        }
        #endregion

        #region Show Details of Product
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProductModel? productModel = await _product_DALBase.GetProductDetails(id);
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
                productModel = await _product_DALBase.GetProductDetails(id);
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
                    await _product_DALBase.AddProduct(product);
                }
                else
                {
                    await _product_DALBase.EditProduct(product);
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
            ProductModel product = await _product_DALBase.GetProductDetails(id);
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
            await _product_DALBase.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
