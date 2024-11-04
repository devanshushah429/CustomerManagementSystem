using Excercise02.Models;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.DAL
{
    public class PartyWiseProduct_DALBase : DAL_Helper
    {
        public async Task<List<PartyWiseProductModel>> GetAllProductOfPartyByID(int id)
        {

            List<PartyWiseProductModel> list = await _context.PartyWiseProduct.Where(p => p.PartyID == id)
                            .Join(  _context.Products,
                                    partyWiseProduct => partyWiseProduct.ProductID, 
                                    product => product.ProductID, 
                                    (partyWiseProduct, product) => new PartyWiseProductModel() { Product = product })
                            .ToListAsync();
            return list;
        }

        public void AddPartyWiseProduct(PartyWiseProductModel partyWiseProductModel)
        {
            _context.PartyWiseProduct.Add(partyWiseProductModel);
            SaveChanges();
        }

        public async Task<List<ProductModel?>> GetProductIDsOfParty(int partyID)
        {
            List<ProductModel> availableProducts = await _context.Products
                .Where(product => !_context.PartyWiseProduct
                .Where(partyWiseProduct => partyWiseProduct.PartyID == partyID)
                .Select(partyWiseProduct => partyWiseProduct.ProductID)
                .Contains(product.ProductID))
                .ToListAsync();
            return availableProducts;
        }
    }
}
