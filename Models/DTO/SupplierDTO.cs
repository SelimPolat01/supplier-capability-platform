using System.ComponentModel.DataAnnotations;

namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class SupplierDTO
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string Surname { get; set; } = string.Empty;

        [Display(Name = "Company Name")]
        public string? CompanyName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Certificates  Count")]
        public int CertificatesCount { get; set; }

        [Display(Name = "Machines  Count")]
        public int MachinesCount { get; set; }

        [Display(Name = "Human Resources  Count")]
        public int HumanResourcesCount { get; set; }

        [Display(Name = "Registration Date")]
        public DateTime CreatedAt { get; set; }
    }
}
