using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplierCapabilitiesAndManagementSystem.Entities
{
    public class MachinePurchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PurchaserId { get; set; }

        [ForeignKey("PurchaserId")]
        public virtual AppUser Purchaser { get; set; } = null!;

        [Required]
        public int SupplierMachineId { get; set; }

        [ForeignKey("SupplierMachineId")]
        public virtual SupplierMachine SupplierMachine { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasedPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPurchasedPrice { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    }

    public static class MachinePurchaseExtentions
    {
        public static MachinePurchaseDTO ToMachinePurchaseDTO(this MachinePurchase machinePurchase)
        {
            if (machinePurchase == null) return null!;

            return new MachinePurchaseDTO()
            {
                Id = machinePurchase.Id,
                Quantity = machinePurchase.Quantity,
                TotalPurchasedPrice = machinePurchase.TotalPurchasedPrice,
                PurchasedPrice = machinePurchase.PurchasedPrice,
                PurchaseDate = machinePurchase.PurchaseDate,
                MachineDetails = machinePurchase.SupplierMachine?.ToSupplierMachineDTO()!
            };
        }
    }
}