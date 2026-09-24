using Microsoft.AspNetCore.Identity;
using SupplierCapabilitiesAndManagementSystem.Enums;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplierCapabilitiesAndManagementSystem.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required.")]
        [MaxLength(50, ErrorMessage = "Surname cannot exceed 50 characters.")]
        public string Surname { get; set; } = string.Empty;

        [MaxLength(150, ErrorMessage = "Company Name cannot exceed 150 characters.")]
        public string CompanyName { get; set; } = string.Empty;

        public int? QualitierId { get; set; }

        [ForeignKey("QualitierId")]
        public virtual AppUser Qualitier { get; set; }

        public QualityScore? QualityScore { get; set; }

        public DateTime? LastScoreUpdate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<SupplierMachine> SupplierMachines { get; set; } = new List<SupplierMachine>();

        public virtual ICollection<SupplierCertificate> SupplierCertificates { get; set; } = new List<SupplierCertificate>();

        public virtual ICollection<SupplierHumanResource> SupplierHumanResources { get; set; } = new List<SupplierHumanResource>();

        public virtual ICollection<PurchaserMachinePurchase> PurchaserMachinePurchases { get; set; } = new List<PurchaserMachinePurchase>();

        public virtual ICollection<SupplierMachine> QualitierSupplierMachineScores { get; set; } = new List<SupplierMachine>();

        public virtual ICollection<SupplierCertificate> QualitierSupplierCertificateScores { get; set; } = new List<SupplierCertificate>();

        public virtual ICollection<SupplierHumanResource> QualitierSupplierHumanResourceScores { get; set; } = new List<SupplierHumanResource>();

        public virtual ICollection<PurchaserMachinePurchase> QualitierPurchaserPurchaseScores { get; set; } = new List<PurchaserMachinePurchase>();
    }

    public static class AppUserExtensions
    {
        public static SupplierDTO ToSupplierDTO(this AppUser supplier)
        {
            if (supplier == null) return null!;

            return new SupplierDTO()
            {
                Id = supplier.Id,
                QualitierId = supplier.QualitierId,
                QualityScore = supplier.QualityScore,
                QualitierFullName = supplier.Qualitier != null ? $"{supplier.Qualitier.Name} {supplier.Qualitier.Surname}" : null,
                LastScoreUpdate = supplier.LastScoreUpdate,
                Name = supplier.Name,
                Surname = supplier.Surname,
                CompanyName = supplier.CompanyName,
                Email = supplier.Email ?? string.Empty,
                CreatedAt = supplier.CreatedAt,
                Certificates = supplier.SupplierCertificates.Select(supplierCertificate => supplierCertificate.ToSupplierCertificateDTO()).ToList(),
                Machines = supplier.SupplierMachines.Select(supplierMachine => supplierMachine.ToSupplierMachineDTO()).ToList(),
                HumanResources = supplier.SupplierHumanResources.Select(supplierHR => supplierHR.ToSupplierHumanResourceDTO()).ToList(),
            };
        }

        public static QualitierDTO ToQualitierDTO(this AppUser qualitier)
        {
            if (qualitier == null) return null!;

            return new QualitierDTO()
            {
                Id = qualitier.Id,
                Name = qualitier.Name,
                Surname = qualitier.Surname,
                Email = qualitier.Email ?? string.Empty,
                CreatedAt = qualitier.CreatedAt,
            };
        }

        public static PurchaserDTO ToPurchaserDTO(this AppUser purchaser)
        {
            if (purchaser == null) return null!;

            return new PurchaserDTO()
            {
                Id = purchaser.Id,
                QualitierId = purchaser.QualitierId,
                QualityScore = purchaser.QualityScore,
                QualitierFullName = purchaser.Qualitier != null ? $"{purchaser.Qualitier.Name} {purchaser.Qualitier.Surname}" : null,
                LastScoreUpdate = purchaser.LastScoreUpdate,
                Name = purchaser.Name,
                Surname = purchaser.Surname,
                Email = purchaser.Email ?? string.Empty,
                Purchases = purchaser.PurchaserMachinePurchases?.Select(machinePurchase => machinePurchase.ToPurchaserMachinePurchaseDTO()).ToList() ?? new List<PurchaserMachinePurchaseDTO>(),
                CreatedAt = purchaser.CreatedAt,
            };
        }
    }
}