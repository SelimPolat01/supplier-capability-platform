using System.ComponentModel.DataAnnotations;

namespace TedarikciKabiliyetYonetimSistemi.Models.DTO
{
    public class EditCertificatePatchRequestDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Issued By")]
        public string IssuedBy { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }
    }
}
