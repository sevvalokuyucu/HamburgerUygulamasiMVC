using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerUygulamasi2.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Siparis_SiparisId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_SiparisId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "SiparisId",
                table: "Menu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SiparisId",
                table: "Menu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_SiparisId",
                table: "Menu",
                column: "SiparisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Siparis_SiparisId",
                table: "Menu",
                column: "SiparisId",
                principalTable: "Siparis",
                principalColumn: "Id");
        }
    }
}
