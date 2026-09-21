namespace SupplierCapabilitiesAndManagementSystem.Models
{
    public class PrimaryButtonModel
    {
        public string Type { get; set; } = "submit";
        public string? Text { get; set; }
        public string? OnClick { get; set; }
        public string? CssClass { get; set; }
        public string? IconClass { get; set; }
    }
}
