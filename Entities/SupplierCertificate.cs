using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;

namespace SupplierCapabilitiesAndManagementSystem.Entities
{
    public class SupplierCertificate
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

    public static class SupplierCertificateExtensions
    {
        public static SupplierCertificate ToSupplierCertificate(this AddSupplierCertificatePostRequestDTO addCertificatePostRequestDTO, int userId, string documentUrl)
        {
            return new SupplierCertificate()
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

        public static AddSupplierCertificatePostResponseDTO ToAddSupplierCertificatePostResponseDTO(this SupplierCertificate certificate, string message, bool isSuccess)
        {
            return new AddSupplierCertificatePostResponseDTO()
            {
                Message = message,
                IsSuccess = isSuccess,
                AddedCertificateId = certificate.Id,
            };
        }
        public static SupplierCertificate ToSupplierCertificate(this EditSupplierCertificatePatchRequestDTO editCertificatePatchRequestDTO, int userId)
        {
            return new SupplierCertificate()
            {
                Id = editCertificatePatchRequestDTO.Id,
                AppUserId = userId,
                Name = editCertificatePatchRequestDTO.Name,
                IssuedBy = editCertificatePatchRequestDTO.IssuedBy,
                IssueDate = editCertificatePatchRequestDTO.IssueDate,
                ExpiryDate = editCertificatePatchRequestDTO.ExpiryDate
            };
        }

        public static SupplierCertificateDTO ToSupplierCertificateDTO(this SupplierCertificate entity)
        {
            if (entity == null) return null!;

            return new SupplierCertificateDTO()
            {
                Id = entity.Id,
                AppUserId = entity.AppUserId,
                Name = entity.Name,
                IssuedBy = entity.IssuedBy,
                IssueDate = entity.IssueDate,
                ExpiryDate = entity.ExpiryDate,
                DocumentUrl = entity.DocumentUrl,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}