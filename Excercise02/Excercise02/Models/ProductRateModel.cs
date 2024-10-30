using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excercise02.Models
{
    public class ProductRateModel
    {
        [Key]
        public int? ProductRateID { get; set; }

        [ForeignKey("Products")]
        public int? ProductID { get; set; }
        public ProductModel? Product { get; set; }

        public decimal? ProductRate { get; set; }

        public DateTime? PriceAppliedDate { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}