using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TedarikciKabiliyetYonetimSistemi.Models.DTO;

namespace TedarikciKabiliyetYonetimSistemi.Entities
{
    public class Certificate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; } = null!;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Issued By")]
        public string IssuedBy { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Issue Date")]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Certificate File")]
        public string DocumentUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public static class CertificateExtensions
    {
        public static Certificate ToCertificate(this AddCertificatePostRequestDTO addCertificatePostRequestDTO, int userId, string documentUrl)
        {
            return new Certificate()
            {
                AppUserId = userId,
                Name = addCertificatePostRequestDTO.Name,
                IssuedBy = addCertificatePostRequestDTO.IssuedBy,
                IssueDate = addCertificatePostRequestDTO.IssueDate!.Value,
                ExpiryDate = addCertificatePostRequestDTO.ExpiryDate,
                DocumentUrl = documentUrl,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static AddCertificatePostResponseDTO ToAddCertificatePostResponseDTO(this Certificate certificate, string message, bool isSuccess)
        {
            return new AddCertificatePostResponseDTO()
            {
                Message = message,
                IsSuccess = isSuccess,
                AddedCertificateId = certificate.Id,
            };
        }

        public static Certificate ToCertificate(this EditCertificatePatchRequestDTO editCertificatePatchRequestDTO, int userId)
        {
            return new Certificate()
            {
                Id = editCertificatePatchRequestDTO.Id,
                AppUserId = userId,
                Name = editCertificatePatchRequestDTO.Name,
                IssuedBy = editCertificatePatchRequestDTO.IssuedBy,
                IssueDate = editCertificatePatchRequestDTO.IssueDate,
                ExpiryDate = editCertificatePatchRequestDTO.ExpiryDate
            };
        }
    }
}