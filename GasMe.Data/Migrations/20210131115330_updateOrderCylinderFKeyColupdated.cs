using Microsoft.EntityFrameworkCore.Migrations;

namespace GasMe.Data.Migrations
{
    public partial class updateOrderCylinderFKeyColupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "capacityCode",
                table: "Cylinder");

            migrationBuilder.DropColumn(
                name: "unitCode",
                table: "Capacity");

            migrationBuilder.RenameColumn(
                name: "currencyCode",
                table: "Cylinder",
                newName: "name");

            migrationBuilder.AddColumn<int>(
                name: "capacityId",
                table: "Cylinder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "currencyId",
                table: "Cylinder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "unitId",
                table: "Capacity",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "capacityId",
                table: "Cylinder");

            migrationBuilder.DropColumn(
                name: "currencyId",
                table: "Cylinder");

            migrationBuilder.DropColumn(
                name: "unitId",
                table: "Capacity");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Cylinder",
                newName: "currencyCode");

            migrationBuilder.AddColumn<string>(
                name: "capacityCode",
                table: "Cylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unitCode",
                table: "Capacity",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
