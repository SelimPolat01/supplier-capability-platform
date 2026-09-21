using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplierCapabilitiesAndManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnsHasBeenAddedToMachinePurchaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PurchaserMachinePurchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPurchasedPrice",
                table: "PurchaserMachinePurchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PurchaserMachinePurchases");

            migrationBuilder.DropColumn(
                name: "TotalPurchasedPrice",
                table: "PurchaserMachinePurchases");
        }
    }
}
