using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerUygulamasi2.Migrations
{
    /// <inheritdoc />
    public partial class m16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EkstraMalzemeId",
                table: "SepetUrun");

            migrationBuilder.CreateTable(
                name: "SepetUrunMalzemeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SepetUrunId = table.Column<int>(type: "int", nullable: false),
                    EkstraMalzemeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SepetUrunMalzemeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SepetUrunMalzemeler_EkstraMalzeme_EkstraMalzemeId",
                        column: x => x.EkstraMalzemeId,
                        principalTable: "EkstraMalzeme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SepetUrunMalzemeler_SepetUrun_SepetUrunId",
                        column: x => x.SepetUrunId,
                        principalTable: "SepetUrun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SepetUrunMalzemeler_EkstraMalzemeId",
                table: "SepetUrunMalzemeler",
                column: "EkstraMalzemeId");

            migrationBuilder.CreateIndex(
                name: "IX_SepetUrunMalzemeler_SepetUrunId",
                table: "SepetUrunMalzemeler",
                column: "SepetUrunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SepetUrunMalzemeler");

            migrationBuilder.AddColumn<int>(
                name: "EkstraMalzemeId",
                table: "SepetUrun",
                type: "int",
                nullable: true);
        }
    }
}
