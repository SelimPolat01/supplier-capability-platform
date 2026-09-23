using SupplierCapabilitiesAndManagementSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class SupplierMachineDTO
    {
        [Display(Name = "Machine ID")]
        public int Id { get; set; }

        [Display(Name = "Supplier ID")]
        public int AppUserId { get; set; }

        [Display(Name = "Qualitier ID")]
        public int? QualitierId { get; set; }

        [Display(Name = "Quality Score")]
        public QualityScore? QualityScore { get; set; }

        [Display(Name = "Qualitier Full Name")]
        public string? QualitierFullName { get; set; }

        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; } = string.Empty;

        [Display(Name = "Supplier Surname")]
        public string SupplierSurname { get; set; } = string.Empty;

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Machine Group")]
        public string MachineGroup { get; set; } = string.Empty;

        [Display(Name = "Machine Type")]
        public string MachineType { get; set; } = string.Empty;

        [Display(Name = "Brand / Model")]
        public string BrandAndModel { get; set; } = string.Empty;

        [Display(Name = "Production Year")]
        public int ProductionYear { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Axis Count")]
        public int? AxisCount { get; set; }

        [Display(Name = "Capacity / Specs")]
        public string CapacitySpecs { get; set; } = string.Empty;

        [Display(Name = "Image File")]
        public string ImageUrl { get; set; } = string.Empty;


        [Display(Name = "Last Score Update")]
        public DateTime? LastScoreUpdate { get; set; }

        [Display(Name = "Added Date")]
        public DateTime CreatedAt { get; set; }
    }
}
