namespace TedarikciKabiliyetYonetimSistemi.Models.DTO
{
    public class RemoveMachineDeleteResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? DeletedMachineId { get; set; }
    }
}
