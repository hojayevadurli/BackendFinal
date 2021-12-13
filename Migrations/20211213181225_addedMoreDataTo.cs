using Microsoft.EntityFrameworkCore.Migrations;

namespace Final.Migrations
{
    public partial class addedMoreDataTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProFileFileName",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProFileFileName",
                table: "Users");
        }
    }
}
