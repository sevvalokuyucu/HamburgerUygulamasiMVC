using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerUygulamasi2.Migrations
{
    /// <inheritdoc />
    public partial class m12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Malzeme_Menu_MenuId",
                table: "Malzeme");

            migrationBuilder.DropIndex(
                name: "IX_Malzeme_MenuId",
                table: "Malzeme");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Malzeme");

            migrationBuilder.CreateTable(
                name: "MalzemeMenu",
                columns: table => new
                {
                    MenuMalzemelerId = table.Column<int>(type: "int", nullable: false),
                    kullanılanMenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalzemeMenu", x => new { x.MenuMalzemelerId, x.kullanılanMenuId });
                    table.ForeignKey(
                        name: "FK_MalzemeMenu_Malzeme_MenuMalzemelerId",
                        column: x => x.MenuMalzemelerId,
                        principalTable: "Malzeme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MalzemeMenu_Menu_kullanılanMenuId",
                        column: x => x.kullanılanMenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeMenu_kullanılanMenuId",
                table: "MalzemeMenu",
                column: "kullanılanMenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MalzemeMenu");

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "Malzeme",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Malzeme_MenuId",
                table: "Malzeme",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Malzeme_Menu_MenuId",
                table: "Malzeme",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id");
        }
    }
}
