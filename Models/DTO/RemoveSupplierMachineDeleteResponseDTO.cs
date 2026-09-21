namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class RemoveSupplierMachineDeleteResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? DeletedMachineId { get; set; }
    }
}
