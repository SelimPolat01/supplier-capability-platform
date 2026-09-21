namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class EditSupplierCertificatePatchResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? EditedCertificateId { get; set; }
    }
}
