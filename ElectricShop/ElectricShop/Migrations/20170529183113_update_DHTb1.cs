using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectricShop.Migrations
{
    public partial class update_DHTb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TenNSXKhongDau",
                table: "NhaSanXuat",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenNSXCoDau",
                table: "NhaSanXuat",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenKhachHang",
                table: "DonHang",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "DonHang",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiaChi",
                table: "DonHang",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DaDuyet",
                table: "DonHang",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DaGiao",
                table: "DonHang",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaDuyet",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "DaGiao",
                table: "DonHang");

            migrationBuilder.AlterColumn<string>(
                name: "TenNSXKhongDau",
                table: "NhaSanXuat",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "TenNSXCoDau",
                table: "NhaSanXuat",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "TenKhachHang",
                table: "DonHang",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "DonHang",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DiaChi",
                table: "DonHang",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
