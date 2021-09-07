using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasMe.Data.Migrations
{
    public partial class updatedColsScheduleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "transactionStatus",
                table: "Schedule");

            migrationBuilder.RenameColumn(
                name: "pickupDate",
                table: "Schedule",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "deliveryDate",
                table: "Schedule",
                newName: "Time");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Schedule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "alias",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "alias",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Schedule");

            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "Schedule",
                newName: "pickupDate");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Schedule",
                newName: "deliveryDate");

            migrationBuilder.AddColumn<byte>(
                name: "transactionStatus",
                table: "Schedule",
                type: "tinyint",
                nullable: true);
        }
    }
}
