using Excercise02.Contexts;
using Excercise02.DAL;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.Controllers
{
    public class PartyController : Controller
    {
        private readonly InterParty_DALBase _InterParty_DALBase;
        public PartyController(InterParty_DALBase InterParty_DALBase)
        {
            InterParty_DALBase = _InterParty_DALBase;
        }
        #region View list of all the parties
        [Route("Party")]
        [HttpGet]
        public async Task<IActionResult> Index(string partyName, string email, string phoneNumber, string sortOrder)
        {
            List<PartyModel> list = await party_DALBase.GetAllPartiesAsync();

            #region Searching
            if (!string.IsNullOrEmpty(partyName))
            {
                list = list.Where(p => p.PartyName.Contains(partyName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(email))
            {
                list = list.Where(p => p.Email.Contains(email, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                list = list.Where(p => p.PhoneNumber.Contains(phoneNumber)).ToList(); // Assuming PhoneNumber is a string
            }
            #endregion

            #region Sorting
            switch (sortOrder)
            {
                case "PartyName":
                    list = list.OrderBy(p => p.PartyName).ToList();
                    break;
                case "Created":
                    list = list.OrderBy(p => p.Created).ToList();
                    break;
                case "Modified":
                    list = list.OrderBy(p => p.Modified).ToList();
                    break;
                case "PhoneNumber":
                default:
                    list = list.OrderBy(p => p.PhoneNumber).ToList();
                    break;
            }
            #endregion

            ViewBag.PartyName = partyName;
            ViewBag.Email = email;
            ViewBag.PhoneNumber = phoneNumber;

            return View(list);
        }
        #endregion

        #region Show Details of party
        [HttpGet]
        public async Task<IActionResult> Details(int partyID)
        {
            PartyModel? party = await InterParty_DALBase.GetPartyDetailsAsync(partyID);
            if (party != null)
            {
                return View(party);
            }
            else
            {
                return NotFound();
            }
        }
        #endregion

        #region Show form to Create new party or Edit the party of given ID
        [HttpGet]
        public async Task<IActionResult> AddEditParty(int? id)
        {
            PartyModel partyModel = new PartyModel();

            if (id.HasValue)
            {
                partyModel = await party_DALBase.GetBasicPartyDetailsAsync(id.Value);

                if (partyModel == null)
                {
                    return NotFound();
                }
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
                try
                {
                    if (partyModel.PartyID == null)
                    {
                        // Case 1: Add new Party
                        await party_DALBase.AddPartyAsync(partyModel); // Assuming AddParty is made async
                    }
                    else
                    {
                        // Case 2: Update existing party
                        await party_DALBase.EditPartyByPartyIDAsync(partyModel); // Assuming EditPartyByPartyID is made async
                    }
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the party: " + ex.Message);
                }
            }
            return View(partyModel);
        }
        #endregion

        #region Open the Confirm Delete Page
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            PartyModel party = await party_DALBase.GetBasicPartyDetailsAsync(id);
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
            await party_DALBase.DeleteThePartyAsync(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Get party wise Invoice
        public async Task<IActionResult> PartyWiseInvoice(int partyID)
        {
            Invoice_DALBase invoice_DALBase = new Invoice_DALBase();
            List<InvoiceModel> list = await invoice_DALBase.GetInvoicesByPartyID(partyID);
            ViewBag.PartyID = partyID;
            return View(list);
        }
        #endregion
    }
}
