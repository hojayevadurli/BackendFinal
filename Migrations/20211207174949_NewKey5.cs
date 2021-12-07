using Microsoft.EntityFrameworkCore.Migrations;

namespace Final.Migrations
{
    public partial class NewKey5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Channels_ChannelId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "ChannelsId",
                table: "Topics");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "Topics",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Channels_ChannelId",
                table: "Topics",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Channels_ChannelId",
                table: "Topics");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "Topics",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ChannelsId",
                table: "Topics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Channels_ChannelId",
                table: "Topics",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
