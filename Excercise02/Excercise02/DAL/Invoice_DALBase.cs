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
        public async Task<InvoiceModel>? GetInvoiceById(int invoiceID)
        {
            InvoiceModel? invoiceModel = await _context.Invoices.FindAsync(invoiceID);
            if(invoiceModel == null)
            {
                return null;
            }
            return invoiceModel;
        }
        #endregion

        #region Add Invoice
        public async Task<InvoiceModel> AddInvoice(InvoiceModel model)
        {
            _context.Invoices.Add(model);
            await SaveChangesAsync();
            return model;
        }
        #endregion
    }
}
