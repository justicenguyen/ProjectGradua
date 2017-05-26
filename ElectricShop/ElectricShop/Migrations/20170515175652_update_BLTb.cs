using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectricShop.Migrations
{
    public partial class update_BLTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaSanPham",
                table: "BinhLuan",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGian",
                table: "BinhLuan",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaSanPham",
                table: "BinhLuan");

            migrationBuilder.DropColumn(
                name: "ThoiGian",
                table: "BinhLuan");
        }
    }
}
