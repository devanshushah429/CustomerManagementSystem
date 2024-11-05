using Excercise02.DAL;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;

namespace Excercise02.Controllers
{
    public class InvoiceWiseProductController : Controller
    {
        private static InvoiceWiseProduct_DALBase invoiceWiseProduct_DAL = new InvoiceWiseProduct_DALBase();

        #region Index Method
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int invoiceWiseProductID)
        {
            int? invoiceID = await invoiceWiseProduct_DAL.DeleteInvoiceWiseProduct(invoiceWiseProductID);
            return RedirectToAction("EditInvoice", "Invoice", new { invoiceID = invoiceID });
        }
        #endregion

        #region Add Invoice Wise Product
        [HttpGet]
        public async Task<IActionResult> AddInvoiceWiseProduct(int invoiceID, int partyID)
        {
            PartyWiseProduct_DALBase partyWiseProduct_DAL = new PartyWiseProduct_DALBase();
            List<PartyWiseProductModel> list = await partyWiseProduct_DAL.GetAllProductOfPartyByID(partyID);
            List<int?> excludeProductsIDs = await invoiceWiseProduct_DAL.GetAllID(invoiceID);
            ViewBag.Products = list.Where(x => !excludeProductsIDs.Contains(x.ProductID));
            ViewBag.InvoiceID = invoiceID;
            return View();
        }
        #endregion

        #region Add Invoice and save to database
        [HttpPost]
        public async Task<IActionResult> AddInvoiceWiseProduct(InvoiceWiseProductModel model)
        {
            if (ModelState.IsValid)
            {
                await invoiceWiseProduct_DAL.AddInvoiceWiseProduct(model);
                return RedirectToAction("Details", "Invoice", new { invoiceID = model.InvoiceID });
            }
            return View(model);
        }
        #endregion

        #region Open Edit Form
        [HttpGet]
        public async Task<IActionResult> Edit(int invoiceWiseProductID)
        {
            InvoiceWiseProductModel invoiceWiseProduct = await invoiceWiseProduct_DAL.GetDetails(invoiceWiseProductID);
            return View(invoiceWiseProduct);
        }
        #endregion

        #region Edit and save to database
        [HttpPost]
        public async Task<IActionResult> Edit(InvoiceWiseProductModel invoiceWiseProductModel)
        {
            await invoiceWiseProduct_DAL.EditInvoiceWiseProduct(invoiceWiseProductModel);
            return RedirectToAction("Details", "Invoice", new { invoiceID = invoiceWiseProductModel.InvoiceID });
        }
        #endregion

    }
}
