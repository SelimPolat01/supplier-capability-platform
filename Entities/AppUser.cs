using Microsoft.AspNetCore.Identity;

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
    }
}
