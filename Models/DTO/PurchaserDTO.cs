using SupplierCapabilitiesAndManagementSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class PurchaserDTO
    {
        [Display(Name = "Purchased Machines")]
        public List<PurchaserMachinePurchaseDTO> Purchases { get; set; } = new();

        [Display(Name = "Purchaser ID")]
        public int Id { get; set; }

        [Display(Name = "Qualitier ID")]
        public int? QualitierId { get; set; }

        [Display(Name = "First Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string Surname { get; set; } = string.Empty;

        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Quality Score")]
        public QualityScore? QualityScore { get; set; }

        [Display(Name = "Qualitier Full Name")]
        public string? QualitierFullName { get; set; }

        [Display(Name = "Last Score Update")]
        public DateTime? LastScoreUpdate { get; set; }

        [Display(Name = "Registration Date")]
        public DateTime CreatedAt { get; set; }

    }
}
