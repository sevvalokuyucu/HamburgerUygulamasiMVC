﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerUygulamasi2.Migrations
{
    /// <inheritdoc />
    public partial class m13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MalzemeAdi",
                table: "Malzeme",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MalzemeAdi",
                table: "Malzeme",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
