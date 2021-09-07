using Microsoft.EntityFrameworkCore.Migrations;

namespace GasMe.Data.Migrations
{
    public partial class updatedPeriodDaysColsScheduleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PeridDays",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeridDays",
                table: "Schedule");
        }
    }
}
