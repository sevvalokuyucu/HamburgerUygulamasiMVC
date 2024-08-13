using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerUygulamasi2.Migrations
{
    /// <inheritdoc />
    public partial class m8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SepetUrun_Siparis_SiparisId",
                table: "SepetUrun");

            migrationBuilder.AlterColumn<int>(
                name: "SiparisId",
                table: "SepetUrun",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SepetUrun_Siparis_SiparisId",
                table: "SepetUrun",
                column: "SiparisId",
                principalTable: "Siparis",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SepetUrun_Siparis_SiparisId",
                table: "SepetUrun");

            migrationBuilder.AlterColumn<int>(
                name: "SiparisId",
                table: "SepetUrun",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SepetUrun_Siparis_SiparisId",
                table: "SepetUrun",
                column: "SiparisId",
                principalTable: "Siparis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
