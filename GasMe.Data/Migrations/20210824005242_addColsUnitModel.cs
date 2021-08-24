using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasMe.Data.Migrations
{
    public partial class addColsUnitModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "code",
                table: "Unit",
                newName: "alias");

            migrationBuilder.AddColumn<byte>(
                name: "clasification",
                table: "Unit",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clasification",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "alias",
                table: "Unit",
                newName: "code");


        }
    }
}
