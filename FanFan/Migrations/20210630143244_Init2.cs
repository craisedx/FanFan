using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFan.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Сhapters");

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChapterText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FanFictionPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapters_FanFictionPosts_FanFictionPostId",
                        column: x => x.FanFictionPostId,
                        principalTable: "FanFictionPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_FanFictionPostId",
                table: "Chapters",
                column: "FanFictionPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.CreateTable(
                name: "Сhapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChapterText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FanFictionPostId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Сhapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Сhapters_FanFictionPosts_FanFictionPostId",
                        column: x => x.FanFictionPostId,
                        principalTable: "FanFictionPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Сhapters_FanFictionPostId",
                table: "Сhapters",
                column: "FanFictionPostId");
        }
    }
}
