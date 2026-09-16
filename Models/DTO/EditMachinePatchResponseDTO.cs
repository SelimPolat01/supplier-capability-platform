namespace TedarikciKabiliyetYonetimSistemi.Models.DTO
{
    public class EditMachinePatchResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? EditedMachineId { get; set; }
    }
}
