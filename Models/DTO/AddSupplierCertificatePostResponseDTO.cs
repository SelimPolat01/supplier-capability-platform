namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class AddSupplierCertificatePostResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? AddedCertificateId { get; set; }
    }
}
