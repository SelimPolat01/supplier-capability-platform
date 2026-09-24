using SupplierCapabilitiesAndManagementSystem.Enums;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplierCapabilitiesAndManagementSystem.Entities
{
    public class SupplierHumanResource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

        public int? QualitierId { get; set; }

        [ForeignKey("QualitierId")]
        public virtual AppUser? Qualitier { get; set; }

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
        public bool HasLaborUnion { get; set; } = false;

        [Display(Name = "Employee Turnover Rate")]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? EmployeeTurnoverRate { get; set; }

        [Display(Name = "Quality Score")]
        public QualityScore? QualityScore { get; set; }

        public DateTime? LastScoreUpdate { get; set; }

        [Display(Name = "Last Updated Date")]
        public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;
    }

    public static class SupplierHumanResourceExtensions
    {
        public static SupplierHumanResource ToSupplierHumanResource(this EditSupplierHumanResourcePatchRequestDTO addSupplierHumanRespourcePostRequestDTO, int userId)
        {
            return new SupplierHumanResource()
            {
                UserId = userId,
                TotalEmployeeCount = addSupplierHumanRespourcePostRequestDTO.TotalEmployeeCount,
                WhiteCollarCount = addSupplierHumanRespourcePostRequestDTO.WhiteCollarCount,
                BlueCollarCount = addSupplierHumanRespourcePostRequestDTO.BlueCollarCount,
                EngineerCount = addSupplierHumanRespourcePostRequestDTO.EngineerCount,
                QualityControlStaffCount = addSupplierHumanRespourcePostRequestDTO.QualityControlStaffCount,
                RndStaffCount = addSupplierHumanRespourcePostRequestDTO.RndStaffCount,
                CertifiedOperatorCount = addSupplierHumanRespourcePostRequestDTO.CertifiedOperatorCount,
                ShiftCount = addSupplierHumanRespourcePostRequestDTO.ShiftCount,
                WorkingDaysPerWeek = addSupplierHumanRespourcePostRequestDTO.WorkingDaysPerWeek,
                HasLaborUnion = addSupplierHumanRespourcePostRequestDTO.HasLaborUnion ?? false,
                EmployeeTurnoverRate = addSupplierHumanRespourcePostRequestDTO.EmployeeTurnoverRate,
                LastUpdatedDate = DateTime.UtcNow,
            };
        }

        public static EditSupplierHumanResourcePatchResponseDTO ToEditSupplierHumanResourcePatchResponseDTO(this SupplierHumanResource supplierHumanResource, string message, bool isSuccess)
        {
            return new EditSupplierHumanResourcePatchResponseDTO()
            {
                Message = message,
                IsSuccess = isSuccess,
                AddedHumanResourceId = supplierHumanResource.Id
            };
        }
        public static FetchSupplierHumanResourceGetResponseDTO ToFetchSupplierHumanResourcesGetResponseDTO(this SupplierHumanResource supplierHumanResource, string message, bool isSuccess)
        {
            return new FetchSupplierHumanResourceGetResponseDTO()
            {
                HumanResource = supplierHumanResource?.ToSupplierHumanResourceDTO(),
                Message = message,
                IsSuccess = isSuccess
            };
        }

        public static SupplierHumanResourceDTO ToSupplierHumanResourceDTO(this SupplierHumanResource entity)
        {
            if (entity == null) return null!;

            return new SupplierHumanResourceDTO()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                QualitierId = entity.QualitierId,
                QualityScore = entity.QualityScore,
                QualitierFullName = entity.Qualitier != null ? $"{entity.Qualitier.Name} {entity.Qualitier.Surname}" : null,
                TotalEmployeeCount = entity.TotalEmployeeCount,
                WhiteCollarCount = entity.WhiteCollarCount,
                BlueCollarCount = entity.BlueCollarCount,
                EngineerCount = entity.EngineerCount,
                QualityControlStaffCount = entity.QualityControlStaffCount,
                RndStaffCount = entity.RndStaffCount,
                CertifiedOperatorCount = entity.CertifiedOperatorCount,
                ShiftCount = entity.ShiftCount,
                WorkingDaysPerWeek = entity.WorkingDaysPerWeek,
                HasLaborUnion = entity.HasLaborUnion,
                EmployeeTurnoverRate = entity.EmployeeTurnoverRate,
                LastScoreUpdate = entity.LastScoreUpdate,
                LastUpdatedDate = entity.LastUpdatedDate
            };
        }
    }
}
