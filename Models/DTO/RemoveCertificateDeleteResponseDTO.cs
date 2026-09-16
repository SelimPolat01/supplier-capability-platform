namespace TedarikciKabiliyetYonetimSistemi.Models.DTO
{
    public class RemoveCertificateDeleteResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? DeletedCertificateId { get; set; }
    }
}
