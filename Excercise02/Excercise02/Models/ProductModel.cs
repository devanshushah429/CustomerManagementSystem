using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excercise02.Models
{
    public class ProductModel
    {
        [Key]
        public int? ProductID { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [MinLength(1,ErrorMessage ="Product Name must not be empty.")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage="Product price is required.")]

        [NotMapped]
        public decimal? ProductPrice { get; set; }

        public string? Description { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public ICollection<ProductRateModel>? ProductRates { get; set; }

        public ICollection<PartyWiseProductModel>? PartyWiseProducts { get; set; }

        public ICollection<InvoiceWiseProductModel>? InvoiceWiseProducts { get; set; }
    }
}
