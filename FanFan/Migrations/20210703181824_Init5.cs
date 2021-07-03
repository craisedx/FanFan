using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFan.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FanFictionPosts_FandomId",
                table: "FanFictionPosts");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Chapters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FanFictionPosts_FandomId",
                table: "FanFictionPosts",
                column: "FandomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FanFictionPosts_FandomId",
                table: "FanFictionPosts");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Chapters");

            migrationBuilder.CreateIndex(
                name: "IX_FanFictionPosts_FandomId",
                table: "FanFictionPosts",
                column: "FandomId",
                unique: true);
        }
    }
}
