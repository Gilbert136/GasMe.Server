using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasMe.Data.Migrations
{
    public partial class addCapacityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "Schedule");

            migrationBuilder.RenameColumn(
                name: "capacity",
                table: "Cylinder",
                newName: "capacityCode");

            migrationBuilder.CreateTable(
                name: "Capacity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Size = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    unitCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capacity", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Capacity");

            migrationBuilder.RenameColumn(
                name: "capacityCode",
                table: "Cylinder",
                newName: "capacity");

            migrationBuilder.AddColumn<string>(
                name: "quantity",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
