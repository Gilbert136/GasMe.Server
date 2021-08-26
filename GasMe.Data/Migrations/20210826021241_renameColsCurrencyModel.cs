using Microsoft.EntityFrameworkCore.Migrations;

namespace GasMe.Data.Migrations
{
    public partial class renameColsCurrencyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "clasification",
                table: "Unit",
                newName: "classification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "classification",
                table: "Unit",
                newName: "clasification");
        }
    }
}
