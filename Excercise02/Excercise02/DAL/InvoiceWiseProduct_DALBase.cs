using Excercise02.Models;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.DAL
{
    public class InvoiceWiseProduct_DALBase : DAL_Helper
    {
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
            return existingProduct;
        }
        #endregion
    }
}
