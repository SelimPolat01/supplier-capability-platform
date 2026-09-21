using System.ComponentModel.DataAnnotations;

namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class EditSupplierHumanResourcePatchRequestDTO
    {
        [Display(Name = "Total Employee Count")]
        public int? TotalEmployeeCount { get; set; }

        [Display(Name = "White Collar Count")]
        public int? WhiteCollarCount { get; set; }

        [Display(Name = "Blue Collar Count")]
        public int? BlueCollarCount { get; set; }

        [Display(Name = "Engineer Count")]
        public int? EngineerCount { get; set; }

        [Display(Name = "Quality Control Staff Count")]
        public int? QualityControlStaffCount { get; set; }

        [Display(Name = "Rnd Staff Count")]
        public int? RndStaffCount { get; set; }

        [Display(Name = "Certified Operator Count")]
        public int? CertifiedOperatorCount { get; set; }

        [Display(Name = "Shift Count")]
        public int? ShiftCount { get; set; }

        [Display(Name = "Working Days Per Week")]
        public int? WorkingDaysPerWeek { get; set; }

        [Display(Name = "Has Labor Union")]
        public bool? HasLaborUnion { get; set; }

        [Display(Name = "Employee Turnover Rate")]
        public decimal? EmployeeTurnoverRate { get; set; }
    }
}
