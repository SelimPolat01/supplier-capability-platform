namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class RemoveSupplierCertificateDeleteResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? DeletedCertificateId { get; set; }
    }
}
