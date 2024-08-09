using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerUygulamasi2.Migrations
{
    /// <inheritdoc />
    public partial class m5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SepetUrun_AspNetUsers_UserId1",
                table: "SepetUrun");

            migrationBuilder.DropIndex(
                name: "IX_SepetUrun_UserId1",
                table: "SepetUrun");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "SepetUrun");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SepetUrun",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "EkstraMalzemeId",
                table: "SepetUrun",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_SepetUrun_UserId",
                table: "SepetUrun",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SepetUrun_AspNetUsers_UserId",
                table: "SepetUrun",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SepetUrun_AspNetUsers_UserId",
                table: "SepetUrun");

            migrationBuilder.DropIndex(
                name: "IX_SepetUrun_UserId",
                table: "SepetUrun");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "SepetUrun",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "EkstraMalzemeId",
                table: "SepetUrun",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "SepetUrun",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SepetUrun_UserId1",
                table: "SepetUrun",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SepetUrun_AspNetUsers_UserId1",
                table: "SepetUrun",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
