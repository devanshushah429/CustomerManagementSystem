using Excercise02.Contexts;
using Excercise02.DAL;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly AppDbContext _context;
        private static Invoice_DALBase invoice_DALBase;
        public InvoiceController(AppDbContext context)
        {
            invoice_DALBase = new Invoice_DALBase();
            _context = context;
        }

        #region Display the List of All Invoices
        public async Task<IActionResult> Index()
        {
            List<InvoiceModel> list = await invoice_DALBase.GetAllInvoices();
            return View(list);
        }
        #endregion

        #region Open Add Invoice Page using PartyID
        [HttpGet]
        public async Task<IActionResult> AddInvoiceUsingPartyID(int partyID)
        {
            InvoiceModel model = new InvoiceModel();
            model.PartyID = partyID;
            model.InvoiceWiseProducts = await _context.PartyWiseProduct
                                        .Where(x => x.PartyID == partyID)
                                        .Join(_context.Parties,
                                                x => x.PartyID,
                                                y => y.PartyID,
                                                (x, y) => new InvoiceWiseProductModel() { Product = x.Product, ProductID = x.ProductID })
                                        .ToListAsync();
            return View("AddEditInvoice", model);
        }
        #endregion

        #region Save The invoice in database table
        [HttpPost]
        public async Task<IActionResult> AddInvoice(InvoiceModel model)
        {
            if (ModelState.IsValid)
            {
                invoice_DALBase.AddInvoice(model);
                return View("Index");
            }
            return View("AddEditInvoice",model);
        }
        #endregion


    }
}
