namespace SupplierCapabilitiesAndManagementSystem.Models
{
    public class ConfirmModalModel
    {
        public string Message { get; set; } = "Confirmation";
        public string Title { get; set; } = "Are you sure you want to proceed?";
        public string? ConfirmButtonText { get; set; } = "Confirm";
        public string? CancelButtonText { get; set; } = "Cancel";
        public string OnConfirm { get; set; } = null!;
        public string? OnCancel { get; set; }
        public string? CssClass { get; set; }
        public string? ConfirmButtonIcon { get; set; }
        public string? CancelButtonIcon { get; set; }
    }
}
