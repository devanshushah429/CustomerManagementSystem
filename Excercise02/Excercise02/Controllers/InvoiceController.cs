using Excercise02.Contexts;
using Excercise02.DAL;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.Controllers
{
    public class InvoiceController : Controller
    {
        private static Invoice_DALBase invoice_DALBase = invoice_DALBase = new Invoice_DALBase();

        #region Display the List of All Invoices
        public async Task<IActionResult> Index()
        {
            List<InvoiceModel> list = await invoice_DALBase.GetAllInvoices();
            return View(list);
        }
        #endregion

        #region Display the Details of Invoice
        public async Task<IActionResult> Details(int invoiceID)
        {
            InvoiceModel invoice = await invoice_DALBase.GetInvoiceDetails(invoiceID);
            return View(invoice);
        }
        #endregion

        #region Open Add Invoice Page using PartyID
        [HttpGet]
        public async Task<IActionResult> AddInvoiceUsingPartyID(int partyID)
        {
            InvoiceModel model = new InvoiceModel();
            model.PartyID = partyID;
            model.InvoiceWiseProducts = await invoice_DALBase.GetInvoiceWiseProducts(partyID);
            return View("AddInvoice", model);
        }
        #endregion

        #region Edit the invoice using InvoiceID
        [HttpGet]
        public async Task<IActionResult> EditInvoice(int invoiceID)
        {
            InvoiceModel? model = await invoice_DALBase.GetInvoiceDetails(invoiceID);
            return View("EditInvoice", model);
        }
        #endregion

        #region Save The invoice in database table
        [HttpPost]
        public async Task<IActionResult> AddEditInvoice(InvoiceModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.InvoiceID == null)
                {
                    await invoice_DALBase.AddInvoice(model);
                }
                else
                {
                    await invoice_DALBase.EditInvoice(model);
                }
                return RedirectToAction("Invoice","Party",new { partyID = model.PartyID });
            }
            return View("AddEditInvoice", model);
        }
        #endregion
    }
}
