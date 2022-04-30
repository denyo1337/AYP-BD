using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class MovemSteamidToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SteamId",
                table: "SteamUserDatas");

            migrationBuilder.AddColumn<string>(
                name: "SteamId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SteamId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "SteamId",
                table: "SteamUserDatas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
