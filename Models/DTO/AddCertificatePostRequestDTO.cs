using System.ComponentModel.DataAnnotations;

namespace TedarikciKabiliyetYonetimSistemi.Models.DTO
{
    public class AddCertificatePostRequestDTO
    {
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Certificate Name / Standard")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Issued By")]
        public string IssuedBy { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Issue Date")]
        [DataType(DataType.Date)]
        public DateTime? IssueDate { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Certificate File (PDF)")]
        public IFormFile? CertificateFile { get; set; }
    }
}