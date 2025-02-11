﻿using Excercise02.Contexts;
using Excercise02.DAL;
using Excercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.Controllers
{
    public class PartyWiseProductController : Controller
    {
        private static PartyWiseProduct_DALBase _partyWiseProduct_DALBase = new PartyWiseProduct_DALBase();

        #region Get the products as per party
        [HttpGet]
        public async Task<List<PartyWiseProductModel>> GetProductsForParty(int id)
        {
            List<PartyWiseProductModel> partyWiseProducts = await _partyWiseProduct_DALBase.GetAllProductOfPartyByID(id);
            return partyWiseProducts;

        }
        #endregion

        #region Open form to add Party Wise Product
        [HttpGet]
        public async Task<IActionResult> AddPartyWiseProduct(int id)
        {
            List<ProductModel?> availableProducts = await _partyWiseProduct_DALBase.GetProductIDsOfParty(id);
            ViewBag.PartyID = id;
            return View(availableProducts);
        }
        #endregion

        #region Add the Product for party to partywise product
        [HttpPost]
        public IActionResult AddPartyWiseProduct(int partyID, int productID)
        {
            PartyWiseProductModel partyWiseProductModel = new PartyWiseProductModel() { ProductID = productID, PartyID = partyID };
            _partyWiseProduct_DALBase.AddPartyWiseProduct(partyWiseProductModel);
            return RedirectToAction("Details", "Party", new { partyID = partyID });
        }
        #endregion

    }
}
