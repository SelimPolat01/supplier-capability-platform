namespace SupplierCapabilitiesAndManagementSystem.Models
{
    public class InputModel
    {
        public string? Name { get; set; }
        public string? Label { get; set; }
        public string? Type { get; set; } = "text";
        public string? Value { get; set; } = string.Empty;
        public string? Placeholder { get; set; }
        public string? InputCssClass { get; set; }
        public string? LabelCssClass { get; set; }
    }
}
