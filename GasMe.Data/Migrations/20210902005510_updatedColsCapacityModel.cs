using Microsoft.EntityFrameworkCore.Migrations;

namespace GasMe.Data.Migrations
{
    public partial class updatedColsCapacityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "Capacity");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "Capacity",
                newName: "value");

            migrationBuilder.CreateIndex(
                name: "IX_Capacity_currencyId",
                table: "Capacity",
                column: "currencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Capacity_unitId",
                table: "Capacity",
                column: "unitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Capacity_Currency_currencyId",
                table: "Capacity",
                column: "currencyId",
                principalTable: "Currency",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Capacity_Unit_unitId",
                table: "Capacity",
                column: "unitId",
                principalTable: "Unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Capacity_Currency_currencyId",
                table: "Capacity");

            migrationBuilder.DropForeignKey(
                name: "FK_Capacity_Unit_unitId",
                table: "Capacity");

            migrationBuilder.DropIndex(
                name: "IX_Capacity_currencyId",
                table: "Capacity");

            migrationBuilder.DropIndex(
                name: "IX_Capacity_unitId",
                table: "Capacity");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "Capacity",
                newName: "size");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "Capacity",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
