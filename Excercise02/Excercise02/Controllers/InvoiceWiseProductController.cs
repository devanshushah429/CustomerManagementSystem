using Excercise02.DAL;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;

namespace Excercise02.Controllers
{
    public class InvoiceWiseProductController : Controller
    {
        private static InvoiceWiseProduct_DALBase invoiceWiseProduct_DAL = new InvoiceWiseProduct_DALBase();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int invoiceWiseProductID)
        {
            int? invoiceID = await invoiceWiseProduct_DAL.DeleteInvoiceWiseProduct(invoiceWiseProductID);
            return RedirectToAction("EditInvoice", "Invoice", new { invoiceID = invoiceID });
        }

        //public async Task<IActionResult> AddInvoiceWiseProduct(int invoiceID)
        //{
        //    InvoiceWiseProductModel model = new InvoiceWiseProductModel() { InvoiceID = i};
        //}


    }
}
