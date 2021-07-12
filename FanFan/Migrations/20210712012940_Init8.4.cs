using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFan.Migrations
{
    public partial class Init84 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_FanFictionPosts_AppUserId",
                table: "FanFictionPosts",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FanFictionPosts_AspNetUsers_AppUserId",
                table: "FanFictionPosts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FanFictionPosts_AspNetUsers_AppUserId",
                table: "FanFictionPosts");

            migrationBuilder.DropIndex(
                name: "IX_FanFictionPosts_AppUserId",
                table: "FanFictionPosts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "FanFictionPosts");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FanFictionPosts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
