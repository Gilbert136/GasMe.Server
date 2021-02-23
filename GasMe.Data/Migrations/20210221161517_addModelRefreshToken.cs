using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasMe.Data.Migrations
{
    public partial class addModelRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    jwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    used = table.Column<bool>(type: "bit", nullable: true),
                    invalidated = table.Column<bool>(type: "bit", nullable: true),
                    identityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.token);
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_identityUserId",
                        column: x => x.identityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_identityUserId",
                table: "RefreshToken",
                column: "identityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");
        }
    }
}
