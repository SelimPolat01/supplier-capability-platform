using Microsoft.AspNetCore.Identity;

namespace TedarikciKabiliyetYonetimSistemi.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? CompanyName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Machine> Machines { get; set; } = new List<Machine>();
        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}
