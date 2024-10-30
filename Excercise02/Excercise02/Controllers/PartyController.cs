using Excercise02.Contexts;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.Controllers
{
    public class PartyController : Controller
    {
        private readonly AppDbContext _context;

        #region Controller
        public PartyController(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region View list of all the parties
        [Route("Party")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PartyModel> list = await _context.Parties.ToListAsync();
            return View(list);
        }
        #endregion

        #region Show Details of party
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            PartyModel? party = await _context.Parties.FindAsync(id);
            PartyWiseProductController partyWiseProductController = new PartyWiseProductController(_context);
            if (party != null)
            {
                party.PartyWiseProducts = await partyWiseProductController.GetProductsForParty(id);
            }
            else
            {
                return NotFound();
            }
            return View(party);
        }
        #endregion

        #region Show form to Create new party or Edit the party of given id
        [HttpGet]
        public async Task<IActionResult> AddEditParty(int? id)
        {
            PartyModel? partyModel = new PartyModel();
            if (id != null)
            {
                partyModel = await _context.Parties.FindAsync(id);
            }
            return View("AddEditPage", partyModel);
        }
        #endregion

        #region Add the new party or edit
        [HttpPost]
        public async Task<IActionResult> AddEditParty(PartyModel partyModel)
        {
            if (ModelState.IsValid)
            {
                if (partyModel.PartyID == null)
                {
                    // Case 1: Add new Party
                    partyModel.Created = DateTime.Now;
                    partyModel.Modified = DateTime.Now;
                    _context.Parties.Add(partyModel);
                }
                else
                {
                    // Case 2: Update existing party
                    var existingParty = await _context.Parties.FindAsync(partyModel.PartyID);
                    if (existingParty != null)
                    {
                        // Update properties
                        existingParty.PartyName = partyModel.PartyName;
                        existingParty.PhoneNumber = partyModel.PhoneNumber;
                        existingParty.Email = partyModel.Email;
                        existingParty.Modified = DateTime.Now;
                        _context.Parties.Update(existingParty);
                    }
                    else
                    {
                        // Case 3: Party does not exist
                        return NotFound();
                    }
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the product: " + ex.Message);
                    return View(partyModel);
                }

                return RedirectToAction("Index");
            }
            return View(partyModel);
        }
        #endregion

        #region Open the Confirm Delete Page
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var party = await _context.Parties.FindAsync(id);
            if (party == null)
            {
                return NotFound();
            }
            return View(party);
        }
        #endregion

        #region Handle POST request to delete a party
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
            var party = await _context.Parties.FindAsync(id);
            if (party == null)
            {
                return NotFound();
            }

            _context.Parties.Remove(party);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Get party wise Invoice
        public async Task<IActionResult> PartyWiseInvoice(int id)
        {
            List<InvoiceModel> list = await _context.Invoices.Where(invoice => invoice.PartyID == id).ToListAsync();
            ViewBag.PartyID = id;
            return View(list);
        }
        #endregion
    }
}
