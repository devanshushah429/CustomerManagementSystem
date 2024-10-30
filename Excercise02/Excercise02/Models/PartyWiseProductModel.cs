using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excercise02.Models
{
    public class PartyWiseProductModel
    {
        [Key]
        public int? PartyWiseProductID { get; set; }

        [ForeignKey("Parties")]
        public int? PartyID { get; set; }

        public PartyModel? Party { get; set; }

        [ForeignKey("Products")]
        public int? ProductID { get; set; }

        public ProductModel? Product { get; set; }
    }
    
}
