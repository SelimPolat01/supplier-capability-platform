using Microsoft.AspNetCore.Identity;
using TedarikciKabiliyetYonetimSistemi.Models.DTO;

namespace TedarikciKabiliyetYonetimSistemi.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? CompanyName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<SupplierMachine> Machines { get; set; } = new List<SupplierMachine>();
        public virtual ICollection<SupplierCertificate> Certificates { get; set; } = new List<SupplierCertificate>();
        public virtual ICollection<SupplierHumanResource> HumanResources { get; set; } = new List<SupplierHumanResource>();
    }

    public static class AppUserExtensions
    {
        public static SupplierDTO ToSupplierDTO(this AppUser supplier)
        {
            if (supplier == null) return null!;

            return new SupplierDTO()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Surname = supplier.Surname,
                CompanyName = supplier.CompanyName,
                Email = supplier.Email ?? string.Empty,
                CreatedAt = supplier.CreatedAt,
                CertificatesCount = supplier.Certificates?.Count ?? 0,
                MachinesCount = supplier.Machines?.Count ?? 0,
                HumanResourcesCount = supplier.HumanResources?.Count ?? 0
            };
        }
    }
}