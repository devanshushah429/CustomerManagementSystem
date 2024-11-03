using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excercise02.Models
{
    public class InvoiceModel
    {
        [Key]
        public int? InvoiceID { get; set; }

        [ForeignKey("Parties")]
        public int? PartyID { get; set; }

        public PartyModel? Party { get; set; }

        public Decimal? TotalPrice { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public bool IsPaid { get; set; } = false;

        public DateTime? Created { get; set; }
        
        public DateTime? Modified { get; set; }

        public List<InvoiceWiseProductModel>? InvoiceWiseProducts { get; set; }

    }
}
