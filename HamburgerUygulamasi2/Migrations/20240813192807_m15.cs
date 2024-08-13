using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerUygulamasi2.Migrations
{
    /// <inheritdoc />
    public partial class m15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EkstraMalzeme_SepetUrun_SepetUrunId",
                table: "EkstraMalzeme");

            migrationBuilder.DropIndex(
                name: "IX_EkstraMalzeme_SepetUrunId",
                table: "EkstraMalzeme");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "EkstraMalzeme");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "EkstraMalzeme");

            migrationBuilder.DropColumn(
                name: "SepetUrunId",
                table: "EkstraMalzeme");

            migrationBuilder.CreateTable(
                name: "EkstraMalzemeSepetUrun",
                columns: table => new
                {
                    ekstraMalzemelerId = table.Column<int>(type: "int", nullable: false),
                    sepetUrunsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkstraMalzemeSepetUrun", x => new { x.ekstraMalzemelerId, x.sepetUrunsId });
                    table.ForeignKey(
                        name: "FK_EkstraMalzemeSepetUrun_EkstraMalzeme_ekstraMalzemelerId",
                        column: x => x.ekstraMalzemelerId,
                        principalTable: "EkstraMalzeme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EkstraMalzemeSepetUrun_SepetUrun_sepetUrunsId",
                        column: x => x.sepetUrunsId,
                        principalTable: "SepetUrun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EkstraMalzemeSepetUrun_sepetUrunsId",
                table: "EkstraMalzemeSepetUrun",
                column: "sepetUrunsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EkstraMalzemeSepetUrun");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "EkstraMalzeme",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "EkstraMalzeme",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SepetUrunId",
                table: "EkstraMalzeme",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EkstraMalzeme_SepetUrunId",
                table: "EkstraMalzeme",
                column: "SepetUrunId");

            migrationBuilder.AddForeignKey(
                name: "FK_EkstraMalzeme_SepetUrun_SepetUrunId",
                table: "EkstraMalzeme",
                column: "SepetUrunId",
                principalTable: "SepetUrun",
                principalColumn: "Id");
        }
    }
}
