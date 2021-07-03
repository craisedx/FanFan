using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFan.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FanFictionPosts_FandomId",
                table: "FanFictionPosts");

            migrationBuilder.CreateIndex(
                name: "IX_FanFictionPosts_FandomId",
                table: "FanFictionPosts",
                column: "FandomId",
                unique: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FanFictionPosts_FandomId",
                table: "FanFictionPosts");

            migrationBuilder.CreateIndex(
                name: "IX_FanFictionPosts_FandomId",
                table: "FanFictionPosts",
                column: "FandomId");
        }
    }
}
