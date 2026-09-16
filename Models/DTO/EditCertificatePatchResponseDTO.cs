namespace TedarikciKabiliyetYonetimSistemi.Models.DTO
{
    public class EditCertificatePatchResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? EditedCertificateId { get; set; }
    }
}
