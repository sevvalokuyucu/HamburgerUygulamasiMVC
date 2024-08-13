using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerUygulamasi2.Migrations
{
    /// <inheritdoc />
    public partial class m6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Açıklama",
                table: "Siparis",
                newName: "Aciklama");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Aciklama",
                table: "Siparis",
                newName: "Açıklama");
        }
    }
}
