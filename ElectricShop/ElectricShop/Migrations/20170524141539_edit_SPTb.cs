using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ElectricShop.Migrations
{
    public partial class edit_SPTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "San_Pham");

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaoHanh = table.Column<int>(nullable: false),
                    BoXuLy = table.Column<string>(nullable: true),
                    ChiaSeThongMinh = table.Column<string>(nullable: true),
                    CongHDMI = table.Column<string>(nullable: true),
                    CongInternet = table.Column<string>(nullable: true),
                    CongSuatLoa = table.Column<string>(nullable: true),
                    CongUSB = table.Column<string>(nullable: true),
                    CongWiFi = table.Column<string>(nullable: true),
                    DoPhanGiai = table.Column<string>(nullable: true),
                    Gia = table.Column<int>(nullable: false),
                    GiaGiam = table.Column<int>(nullable: false),
                    HeDeHanh = table.Column<string>(nullable: true),
                    HienThi = table.Column<int>(nullable: false),
                    HinhAnh = table.Column<string>(nullable: false),
                    KhoiLuongThung = table.Column<string>(nullable: true),
                    KichThuocMH = table.Column<string>(nullable: true),
                    KichThuocThung = table.Column<string>(nullable: true),
                    LoaiDanMay = table.Column<string>(nullable: true),
                    LoaiDauDia = table.Column<string>(nullable: true),
                    LoaiSanPham = table.Column<int>(nullable: false),
                    ManHinhCong = table.Column<string>(nullable: true),
                    MauSac = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NhaSanXuat = table.Column<int>(nullable: false),
                    SanPhamBanChay = table.Column<int>(nullable: false),
                    SmartTV = table.Column<string>(nullable: true),
                    SoLuong = table.Column<int>(nullable: false),
                    TanSoQuet = table.Column<string>(nullable: true),
                    TenSPCoDau = table.Column<string>(nullable: false),
                    TenSPKhongDau = table.Column<string>(nullable: false),
                    TrinhDuyetWeb = table.Column<string>(nullable: true),
                    XuatXu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.CreateTable(
                name: "San_Pham",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaoHanh = table.Column<int>(nullable: false),
                    BoXuLy = table.Column<string>(nullable: true),
                    ChiaSeThongMinh = table.Column<string>(nullable: true),
                    CongHDMI = table.Column<string>(nullable: true),
                    CongInternet = table.Column<string>(nullable: true),
                    CongSuatLoa = table.Column<string>(nullable: true),
                    CongUSB = table.Column<string>(nullable: true),
                    CongWiFi = table.Column<string>(nullable: true),
                    DoPhanGiai = table.Column<string>(nullable: true),
                    Gia = table.Column<int>(nullable: false),
                    GiaGiam = table.Column<int>(nullable: false),
                    HeDeHanh = table.Column<string>(nullable: true),
                    HienThi = table.Column<int>(nullable: false),
                    HinhAnh = table.Column<string>(nullable: false),
                    KhoiLuongThung = table.Column<string>(nullable: true),
                    KichThuocMH = table.Column<string>(nullable: true),
                    KichThuocThung = table.Column<string>(nullable: true),
                    LoaiDanMay = table.Column<string>(nullable: true),
                    LoaiDauDia = table.Column<string>(nullable: true),
                    LoaiSanPham = table.Column<int>(nullable: false),
                    ManHinhCong = table.Column<string>(nullable: true),
                    MauSac = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NhaSanXuat = table.Column<int>(nullable: false),
                    SanPhamBanChay = table.Column<int>(nullable: false),
                    SmartTV = table.Column<string>(nullable: true),
                    SoLuong = table.Column<int>(nullable: false),
                    TanSoQuet = table.Column<string>(nullable: true),
                    TenSPCoDau = table.Column<string>(nullable: false),
                    TenSPKhongDau = table.Column<string>(nullable: false),
                    TrinhDuyetWeb = table.Column<string>(nullable: true),
                    XuatXu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_San_Pham", x => x.ID);
                });
        }
    }
}
