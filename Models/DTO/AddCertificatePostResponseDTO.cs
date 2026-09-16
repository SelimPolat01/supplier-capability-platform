namespace TedarikciKabiliyetYonetimSistemi.Models.DTO
{
    public class AddCertificatePostResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? AddedCertificateId { get; set; }
    }
}
