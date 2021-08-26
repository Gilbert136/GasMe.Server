using Microsoft.EntityFrameworkCore.Migrations;

namespace GasMe.Data.Migrations
{
    public partial class addColsCurrencyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "Currency");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Currency",
                newName: "rate");

            migrationBuilder.AddColumn<int>(
                name: "unitId",
                table: "Currency",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unitId",
                table: "Currency");

            migrationBuilder.RenameColumn(
                name: "rate",
                table: "Currency",
                newName: "Rate");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "Currency",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
