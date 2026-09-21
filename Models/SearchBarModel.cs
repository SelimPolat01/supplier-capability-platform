namespace SupplierCapabilitiesAndManagementSystem.Models
{
    public class SearchBarModel
    {
        public string? Name { get; set; }
        public string? Value { get; set; } = string.Empty;
        public string? Placeholder { get; set; }
        public string? CssClass { get; set; }
        public string ButtonType { get; set; } = "submit";
        public string AspController { get; set; } = null!;
        public string AspAction { get; set; } = null!;
    }
}
