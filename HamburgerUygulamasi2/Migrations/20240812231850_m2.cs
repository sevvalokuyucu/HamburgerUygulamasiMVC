using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerUygulamasi2.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OnayliSiparisVarMi",
                table: "Siparis",
                newName: "SiparisOnayliMi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SiparisOnayliMi",
                table: "Siparis",
                newName: "OnayliSiparisVarMi");
        }
    }
}
