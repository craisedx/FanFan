using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFan.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Сhapters_FanFictionPosts_FanFictionPostId",
                table: "Сhapters");

            migrationBuilder.DropColumn(
                name: "FanFactionPostId",
                table: "Сhapters");

            migrationBuilder.AlterColumn<int>(
                name: "FanFictionPostId",
                table: "Сhapters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Сhapters_FanFictionPosts_FanFictionPostId",
                table: "Сhapters",
                column: "FanFictionPostId",
                principalTable: "FanFictionPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Сhapters_FanFictionPosts_FanFictionPostId",
                table: "Сhapters");

            migrationBuilder.AlterColumn<int>(
                name: "FanFictionPostId",
                table: "Сhapters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FanFactionPostId",
                table: "Сhapters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Сhapters_FanFictionPosts_FanFictionPostId",
                table: "Сhapters",
                column: "FanFictionPostId",
                principalTable: "FanFictionPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
