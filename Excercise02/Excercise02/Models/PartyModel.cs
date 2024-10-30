using System.ComponentModel.DataAnnotations;

namespace Excercise02.Models
{
    public class PartyModel
    {
        [Key]
        public int? PartyID { get; set; }
        
        [Required(ErrorMessage = "Party name is required.")]
        [MinLength(1, ErrorMessage = "Party name cannot be empty.")]
        public string? PartyName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public DateTime? Created { get; set; } 
        
        public DateTime? Modified { get; set; } 
        
        public List<PartyWiseProductModel>? PartyWiseProducts { get; set; }

        public List<InvoiceModel>? Invoices { get; set; }

    }
}
