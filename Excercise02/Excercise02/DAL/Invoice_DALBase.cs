using Excercise02.Models;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.DAL
{
    public class Invoice_DALBase : DAL_Helper
    {
        #region Get All Invoices
        public async Task<List<InvoiceModel>> GetAllInvoices()
        {
            List<InvoiceModel> list = await _context.Invoices.ToListAsync();
            return list;
        }
        #endregion

        #region Get Invoices Of Specific Party Using Party ID
        public async Task<List<InvoiceModel>> GetInvoicesByPartyID(int partyID)
        {
            List<InvoiceModel> list = await _context.Invoices.Where(invoice => invoice.PartyID == partyID).ToListAsync();
            return list;

        }
        #endregion

        #region Get Invoice Details By Invoice ID
        public async Task<InvoiceModel>? GetInvoiceDetails(int invoiceID)
        {
            InvoiceModel? invoiceModel = await _context.Invoices
                .Include(i => i.InvoiceWiseProducts)
                .ThenInclude(iwp => iwp.Product)
                .FirstOrDefaultAsync(i => i.InvoiceID == invoiceID);
            return invoiceModel;
        }
        #endregion

        #region Add Invoice
        public async Task<InvoiceModel> AddInvoice(InvoiceModel model)
        {
            model.Created = DateTime.Now;
            model.Modified = DateTime.Now;
            List<InvoiceWiseProductModel> invoiceWiseProducts = model.InvoiceWiseProducts;
            List<InvoiceWiseProductModel> selectedList = invoiceWiseProducts.Where(invoiceWiseProducts => invoiceWiseProducts.IsSelected).ToList();
            Decimal? totalPrice = 0;
            Product_DALBase product_DALBase = new Product_DALBase();
            foreach (InvoiceWiseProductModel iwp in selectedList)
            {
                ProductModel product = await product_DALBase.GetProductDetails(iwp.ProductID);
                iwp.InvoiceID = model.InvoiceID;
                iwp.Subtotal = iwp.Quantity * product.ProductPrice;
                totalPrice += iwp.Subtotal;
            }
            model.InvoiceWiseProducts = selectedList;
            model.TotalPrice = totalPrice;
            _context.Invoices.Add(model);
            await SaveChangesAsync();
            return model;
        }
        #endregion

        #region Edit Invoice
        public async Task EditInvoice(InvoiceModel model)
        {

        }
        #endregion

        #region Get All Invoice of Specific Party
        public async Task<List<InvoiceWiseProductModel>> GetInvoiceWiseProducts(int partyID)
        {
            List<PartyWiseProductModel> partyWiseProducts = await _context.PartyWiseProduct
                                        .Where(x => x.PartyID == partyID)
                                        .ToListAsync();

            List<InvoiceWiseProductModel> invoiceWiseProducts = new List<InvoiceWiseProductModel>();

            Product_DALBase product_DALBase = new Product_DALBase();
            foreach (var partyProduct in partyWiseProducts)
            {
                ProductModel productDetails = await product_DALBase.GetProductDetails(partyProduct.ProductID);
                invoiceWiseProducts.Add(new InvoiceWiseProductModel
                {
                    Product = productDetails,
                    ProductID = partyProduct.ProductID
                });
            }

            return invoiceWiseProducts;
        }
        #endregion

    }
}
