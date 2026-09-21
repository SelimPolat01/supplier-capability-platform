namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class MachinePurchaseDTO
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPurchasedPrice { get; set; }

        public decimal PurchasedPrice { get; set; }

        public DateTime PurchaseDate { get; set; }

        public SupplierMachineDTO MachineDetails { get; set; } = null!;
    }
}
