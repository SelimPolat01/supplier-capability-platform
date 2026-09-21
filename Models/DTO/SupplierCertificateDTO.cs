using System.ComponentModel.DataAnnotations;

namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class SupplierCertificateDTO
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }

        [Display(Name = "Certificate Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Issued By")]
        public string IssuedBy { get; set; } = string.Empty;

        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Certificate File")]
        public string DocumentUrl { get; set; } = string.Empty;

        [Display(Name = "Added Date")]
        public DateTime CreatedAt { get; set; }
    }
}
