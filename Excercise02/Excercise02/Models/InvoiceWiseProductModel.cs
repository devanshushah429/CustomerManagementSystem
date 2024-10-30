using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excercise02.Models
{
    public class InvoiceWiseProductModel
    {
        [Key]
        public int? InvoiceWiseProductID { get; set; }

        [ForeignKey("Products")]
        public int? ProductID { get; set; }
        public ProductModel? Product { get; set; }


        [ForeignKey("Invoices")]
        public int? InvoiceID { get; set; }

        public InvoiceModel? Invoice { get; set; }

        public int? Quantity { get; set; }

        public Decimal? Subtotal { get; set; }
    }
}
