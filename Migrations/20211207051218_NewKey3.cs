using Microsoft.EntityFrameworkCore.Migrations;

namespace Final.Migrations
{
    public partial class NewKey3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChannelsId",
                table: "Topics",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelsId",
                table: "Topics");
        }
    }
}
