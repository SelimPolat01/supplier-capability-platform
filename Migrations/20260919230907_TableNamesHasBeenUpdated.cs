using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplierCapabilitiesAndManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class TableNamesHasBeenUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_AspNetUsers_AppUserId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_HumanResources_AspNetUsers_UserId",
                table: "HumanResources");

            migrationBuilder.DropForeignKey(
                name: "FK_MachinePurchases_AspNetUsers_PurchaserId",
                table: "MachinePurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_MachinePurchases_Machines_SupplierMachineId",
                table: "MachinePurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_AspNetUsers_AppUserId",
                table: "Machines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Machines",
                table: "Machines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MachinePurchases",
                table: "MachinePurchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HumanResources",
                table: "HumanResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificates",
                table: "Certificates");

            migrationBuilder.RenameTable(
                name: "Machines",
                newName: "SupplierMachines");

            migrationBuilder.RenameTable(
                name: "MachinePurchases",
                newName: "PurchaserMachinePurchases");

            migrationBuilder.RenameTable(
                name: "HumanResources",
                newName: "SupplierHumanResources");

            migrationBuilder.RenameTable(
                name: "Certificates",
                newName: "SupplierCertificates");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_AppUserId_MachineGroup_MachineType_BrandAndModel_ProductionYear_CapacitySpecs",
                table: "SupplierMachines",
                newName: "IX_SupplierMachines_AppUserId_MachineGroup_MachineType_BrandAndModel_ProductionYear_CapacitySpecs");

            migrationBuilder.RenameIndex(
                name: "IX_MachinePurchases_SupplierMachineId",
                table: "PurchaserMachinePurchases",
                newName: "IX_PurchaserMachinePurchases_SupplierMachineId");

            migrationBuilder.RenameIndex(
                name: "IX_MachinePurchases_PurchaserId",
                table: "PurchaserMachinePurchases",
                newName: "IX_PurchaserMachinePurchases_PurchaserId");

            migrationBuilder.RenameIndex(
                name: "IX_HumanResources_UserId",
                table: "SupplierHumanResources",
                newName: "IX_SupplierHumanResources_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificates_AppUserId_Name_IssuedBy_IssueDate_ExpiryDate",
                table: "SupplierCertificates",
                newName: "IX_SupplierCertificates_AppUserId_Name_IssuedBy_IssueDate_ExpiryDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierMachines",
                table: "SupplierMachines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaserMachinePurchases",
                table: "PurchaserMachinePurchases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierHumanResources",
                table: "SupplierHumanResources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierCertificates",
                table: "SupplierCertificates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaserMachinePurchases_AspNetUsers_PurchaserId",
                table: "PurchaserMachinePurchases",
                column: "PurchaserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaserMachinePurchases_SupplierMachines_SupplierMachineId",
                table: "PurchaserMachinePurchases",
                column: "SupplierMachineId",
                principalTable: "SupplierMachines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCertificates_AspNetUsers_AppUserId",
                table: "SupplierCertificates",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierHumanResources_AspNetUsers_UserId",
                table: "SupplierHumanResources",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierMachines_AspNetUsers_AppUserId",
                table: "SupplierMachines",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaserMachinePurchases_AspNetUsers_PurchaserId",
                table: "PurchaserMachinePurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaserMachinePurchases_SupplierMachines_SupplierMachineId",
                table: "PurchaserMachinePurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCertificates_AspNetUsers_AppUserId",
                table: "SupplierCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierHumanResources_AspNetUsers_UserId",
                table: "SupplierHumanResources");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierMachines_AspNetUsers_AppUserId",
                table: "SupplierMachines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierMachines",
                table: "SupplierMachines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierHumanResources",
                table: "SupplierHumanResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierCertificates",
                table: "SupplierCertificates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaserMachinePurchases",
                table: "PurchaserMachinePurchases");

            migrationBuilder.RenameTable(
                name: "SupplierMachines",
                newName: "Machines");

            migrationBuilder.RenameTable(
                name: "SupplierHumanResources",
                newName: "HumanResources");

            migrationBuilder.RenameTable(
                name: "SupplierCertificates",
                newName: "Certificates");

            migrationBuilder.RenameTable(
                name: "PurchaserMachinePurchases",
                newName: "MachinePurchases");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierMachines_AppUserId_MachineGroup_MachineType_BrandAndModel_ProductionYear_CapacitySpecs",
                table: "Machines",
                newName: "IX_Machines_AppUserId_MachineGroup_MachineType_BrandAndModel_ProductionYear_CapacitySpecs");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierHumanResources_UserId",
                table: "HumanResources",
                newName: "IX_HumanResources_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierCertificates_AppUserId_Name_IssuedBy_IssueDate_ExpiryDate",
                table: "Certificates",
                newName: "IX_Certificates_AppUserId_Name_IssuedBy_IssueDate_ExpiryDate");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaserMachinePurchases_SupplierMachineId",
                table: "MachinePurchases",
                newName: "IX_MachinePurchases_SupplierMachineId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaserMachinePurchases_PurchaserId",
                table: "MachinePurchases",
                newName: "IX_MachinePurchases_PurchaserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Machines",
                table: "Machines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HumanResources",
                table: "HumanResources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificates",
                table: "Certificates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MachinePurchases",
                table: "MachinePurchases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_AspNetUsers_AppUserId",
                table: "Certificates",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HumanResources_AspNetUsers_UserId",
                table: "HumanResources",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachinePurchases_AspNetUsers_PurchaserId",
                table: "MachinePurchases",
                column: "PurchaserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MachinePurchases_Machines_SupplierMachineId",
                table: "MachinePurchases",
                column: "SupplierMachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_AspNetUsers_AppUserId",
                table: "Machines",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
