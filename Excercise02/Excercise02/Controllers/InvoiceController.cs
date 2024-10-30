using Excercise02.Contexts;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly AppDbContext _context;
        public InvoiceController(AppDbContext context) {
            _context = context;
        }  
        public async Task<IActionResult> Index()
        {
            List<InvoiceModel> list = await _context.Invoices.ToListAsync();
            return View(list);
        }

        public IActionResult AddEditInvoice(int id)
        {
            return View();
        }


    }
}
