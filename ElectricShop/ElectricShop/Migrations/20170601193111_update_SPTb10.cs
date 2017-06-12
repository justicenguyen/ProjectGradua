using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectricShop.Migrations
{
    public partial class update_SPTb10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gia",
                table: "SanPham");

            migrationBuilder.AddColumn<int>(
                name: "GiaBan",
                table: "SanPham",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GiaGoc",
                table: "SanPham",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "KhachHang",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HoTenKhachHang",
                table: "KhachHang",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiaChi",
                table: "KhachHang",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaBan",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "GiaGoc",
                table: "SanPham");

            migrationBuilder.AddColumn<int>(
                name: "Gia",
                table: "SanPham",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "KhachHang",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "HoTenKhachHang",
                table: "KhachHang",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DiaChi",
                table: "KhachHang",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
