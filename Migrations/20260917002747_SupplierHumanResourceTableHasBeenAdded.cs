using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplierCapabilitiesAndManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class SupplierHumanResourceTableHasBeenAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Machines_AppUserId_MachineGroup_MachineType_BrandAndModel_ProductionYear",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_AppUserId_Name_IssueDate_ExpiryDate",
                table: "Certificates");

            migrationBuilder.AlterColumn<string>(
                name: "CapacitySpecs",
                table: "Machines",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IssuedBy",
                table: "Certificates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "HumanResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalEmployeeCount = table.Column<int>(type: "int", nullable: false),
                    WhiteCollarCount = table.Column<int>(type: "int", nullable: false),
                    BlueCollarCount = table.Column<int>(type: "int", nullable: false),
                    EngineerCount = table.Column<int>(type: "int", nullable: false),
                    QualityControlStaffCount = table.Column<int>(type: "int", nullable: false),
                    RndStaffCount = table.Column<int>(type: "int", nullable: false),
                    CertifiedOperatorCount = table.Column<int>(type: "int", nullable: false),
                    ShiftCount = table.Column<int>(type: "int", nullable: false),
                    WorkingDaysPerWeek = table.Column<int>(type: "int", nullable: false),
                    HasLaborUnion = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeTurnoverRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumanResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HumanResources_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Machines_AppUserId_MachineGroup_MachineType_BrandAndModel_ProductionYear_CapacitySpecs",
                table: "Machines",
                columns: new[] { "AppUserId", "MachineGroup", "MachineType", "BrandAndModel", "ProductionYear", "CapacitySpecs" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_AppUserId_Name_IssuedBy_IssueDate_ExpiryDate",
                table: "Certificates",
                columns: new[] { "AppUserId", "Name", "IssuedBy", "IssueDate", "ExpiryDate" });

            migrationBuilder.CreateIndex(
                name: "IX_HumanResources_UserId",
                table: "HumanResources",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HumanResources");

            migrationBuilder.DropIndex(
                name: "IX_Machines_AppUserId_MachineGroup_MachineType_BrandAndModel_ProductionYear_CapacitySpecs",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_AppUserId_Name_IssuedBy_IssueDate_ExpiryDate",
                table: "Certificates");

            migrationBuilder.AlterColumn<string>(
                name: "CapacitySpecs",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "IssuedBy",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_AppUserId_MachineGroup_MachineType_BrandAndModel_ProductionYear",
                table: "Machines",
                columns: new[] { "AppUserId", "MachineGroup", "MachineType", "BrandAndModel", "ProductionYear" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_AppUserId_Name_IssueDate_ExpiryDate",
                table: "Certificates",
                columns: new[] { "AppUserId", "Name", "IssueDate", "ExpiryDate" });
        }
    }
}
