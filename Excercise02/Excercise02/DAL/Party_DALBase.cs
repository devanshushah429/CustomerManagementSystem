using Excercise02.Controllers;
using Excercise02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Excercise02.DAL
{
    public class Party_DALBase : DAL_Helper
    {
        #region Get All Parties
        public async Task<List<PartyModel>> GetAllPartiesAsync()
        {
            List<PartyModel> list = await _context.Parties.ToListAsync();
            return list;
        }
        #endregion

        #region Get Basic Details
        public async Task<PartyModel>? GetBasicPartyDetailsAsync(int? id)
        {
            PartyModel partyModel = await _context.Parties.FindAsync(id);
            return partyModel;
        }
        #endregion

        #region Get Complete Details of Party
        public async Task<PartyModel>? GetPartyDetailsAsync(int id)
        {
            PartyModel? party = await _context.Parties.FindAsync(id);
            PartyWiseProductController partyWiseProductController = new PartyWiseProductController();
            if (party != null)
            {
                party.PartyWiseProducts = await partyWiseProductController.GetProductsForParty(id);
            }
            return party;
        }
        #endregion

        #region Add the party in party table
        public async Task<PartyModel> AddPartyAsync(PartyModel partyModel)
        {
            partyModel.Created = DateTime.Now;
            partyModel.Modified = DateTime.Now;
            _context.Parties.Add(partyModel);
            await SaveChangesAsync();
            return partyModel;
        }
        #endregion

        #region Edit the party of specific ID
        public async Task<PartyModel?> EditPartyByPartyIDAsync(PartyModel partyModel)
        {
            if (partyModel == null)
            {
                throw new ArgumentNullException(nameof(partyModel), "Party model cannot be null.");
            }

            PartyModel? existingParty = await _context.Parties.FindAsync(partyModel.PartyID);

            if (existingParty == null)
            {
                return null;
            }

            existingParty.PartyName = partyModel.PartyName;
            existingParty.PhoneNumber = partyModel.PhoneNumber;
            existingParty.Email = partyModel.Email;
            existingParty.Modified = DateTime.Now;

            await SaveChangesAsync();

            return existingParty;
        }
        #endregion

        #region Delete the party
        public async Task<bool> DeleteThePartyAsync(int id)
        {

            PartyModel? existingParty = await GetBasicPartyDetailsAsync(id);
            if (existingParty == null)
            {
                return false;
            }

            try
            {
                _context.Parties.Remove(existingParty);
                await SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }
        #endregion
    }
}
