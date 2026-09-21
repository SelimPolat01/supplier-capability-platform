using System.ComponentModel.DataAnnotations;

namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class PurchaserDTO
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string Surname { get; set; } = string.Empty;

        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Purchased Machines")]
        public List<MachinePurchaseDTO> Purchases { get; set; } = new();

        [Display(Name = "Registration Date")]
        public DateTime CreatedAt { get; set; }

    }
}
