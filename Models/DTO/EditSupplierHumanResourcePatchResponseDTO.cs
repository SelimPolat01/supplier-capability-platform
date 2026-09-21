namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class EditSupplierHumanResourcePatchResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? AddedHumanResourceId { get; set; }
    }
}
