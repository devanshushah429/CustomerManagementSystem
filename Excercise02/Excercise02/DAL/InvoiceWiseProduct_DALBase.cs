using Excercise02.Models;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.DAL
{
    public class InvoiceWiseProduct_DALBase : DAL_Helper
    {
        #region Get The List of product ID to exclude
        public async Task<List<int?>> GetAllID(int invoiceID)
        {
            List<int?> ids = await _context.InvoiceWiseProducts.Where(iwp => iwp.InvoiceID == invoiceID).Select(iwp => iwp.ProductID).ToListAsync();
            return ids;
        }
        #endregion

        #region Add Invoice Wise Product
        public async Task AddInvoiceWiseProduct(InvoiceWiseProductModel model)
        {
            Product_DALBase? product_DALBase = new Product_DALBase();
            ProductModel? productModel = await product_DALBase.GetProductDetails(model.ProductID);
            model.Subtotal = model.Quantity * productModel.ProductPrice;
            _context.InvoiceWiseProducts.Add(model);
            InvoiceModel? invoiceModel = await _context.Invoices.FindAsync(model.InvoiceID);
            invoiceModel.TotalPrice += model.Subtotal;
            SaveChangesAsync();
        }
        #endregion

        #region Edit Invoice Product 
        public async Task EditInvoiceWiseProduct(InvoiceWiseProductModel model)
        {
            Product_DALBase? product_DALBase = new Product_DALBase();
            ProductModel? productModel = await product_DALBase.GetProductDetails(model.ProductID);

            model.Subtotal = model.Quantity * productModel.ProductPrice;

            InvoiceWiseProductModel? existingProduct = await _context.InvoiceWiseProducts
                .FirstOrDefaultAsync(iwp => iwp.InvoiceWiseProductID == model.InvoiceWiseProductID);

            InvoiceModel? invoiceModel = await _context.Invoices.FindAsync(existingProduct.InvoiceID);
            invoiceModel.TotalPrice += (model.Subtotal - existingProduct.Subtotal);



            existingProduct.Quantity = model.Quantity;

            existingProduct.Subtotal = model.Subtotal;
            _context.InvoiceWiseProducts.Update(existingProduct);

            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete Invoice Wise Product
        public async Task<int?> DeleteInvoiceWiseProduct(int invoiceWiseProductID)
        {
            InvoiceWiseProductModel? model = await GetDetails(invoiceWiseProductID);
            InvoiceModel? invoiceModel = await _context.Invoices.FindAsync(model.InvoiceID);
            invoiceModel.TotalPrice -= model.Subtotal;
            _context.InvoiceWiseProducts.Remove(model);
            SaveChangesAsync();
            return model.InvoiceID;
        }
        #endregion

        #region Get Details
        public async Task<InvoiceWiseProductModel> GetDetails(int invoiceWiseProductID)
        {
            InvoiceWiseProductModel? existingProduct = await _context.InvoiceWiseProducts
                .FirstOrDefaultAsync(iwp => iwp.InvoiceWiseProductID == invoiceWiseProductID);
            Product_DALBase product_DALBase = new Product_DALBase();
            existingProduct.Product = await product_DALBase.GetProductDetails(existingProduct.ProductID);
            return existingProduct;
        }
        #endregion
    }
}
