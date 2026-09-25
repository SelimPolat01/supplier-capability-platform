using System.ComponentModel.DataAnnotations;

namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class QualitierDTO
    {
        public List<SupplierDTO> ScoredSuppliers { get; set; } = new();
        public List<SupplierCertificateDTO> ScoredSupplierCertificates { get; set; } = new();
        public List<SupplierMachineDTO> ScoredSupplierMachines { get; set; } = new();
        public List<SupplierHumanResourceDTO> ScoredSupplierHumanResource { get; set; } = new();
        public List<PurchaserMachinePurchaseDTO> ScoredPurchaserMachinePurchases { get; set; } = new();
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string Surname { get; set; } = string.Empty;

        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Registration Date")]
        public DateTime CreatedAt { get; set; }
    }
}
