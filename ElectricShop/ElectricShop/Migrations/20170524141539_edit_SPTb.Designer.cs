using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ElectricShop.Models;

namespace ElectricShop.Migrations
{
    [DbContext(typeof(ElectricShopContext))]
    [Migration("20170524141539_edit_SPTb")]
    partial class edit_SPTb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ElectricShop.Models.BinhLuan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("HoTen")
                        .IsRequired();

                    b.Property<int>("MaSanPham");

                    b.Property<string>("NoiDung")
                        .IsRequired();

                    b.Property<string>("SoDienThoai");

                    b.Property<DateTime>("ThoiGian");

                    b.HasKey("ID");

                    b.ToTable("BinhLuan");
                });

            modelBuilder.Entity("ElectricShop.Models.KhuyenMai", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HienThi");

                    b.Property<string>("HinhAnh")
                        .IsRequired();

                    b.Property<string>("NoiDung");

                    b.Property<string>("TieuDeKhuyenMai")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("KhuyenMai");
                });

            modelBuilder.Entity("ElectricShop.Models.LoaiSanPham", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TenLoaiSPCoDau")
                        .IsRequired();

                    b.Property<string>("TenLoaiSPKhongDau")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("LoaiSanPham");
                });

            modelBuilder.Entity("ElectricShop.Models.NhaSanXuat", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TenNSXCoDau");

                    b.Property<string>("TenNSXKhongDau");

                    b.HasKey("ID");

                    b.ToTable("NhaSanXuat");
                });

            modelBuilder.Entity("ElectricShop.Models.SanPham", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BaoHanh");

                    b.Property<string>("BoXuLy");

                    b.Property<string>("ChiaSeThongMinh");

                    b.Property<string>("CongHDMI");

                    b.Property<string>("CongInternet");

                    b.Property<string>("CongSuatLoa");

                    b.Property<string>("CongUSB");

                    b.Property<string>("CongWiFi");

                    b.Property<string>("DoPhanGiai");

                    b.Property<int?>("Gia")
                        .IsRequired();

                    b.Property<int>("GiaGiam");

                    b.Property<string>("HeDeHanh");

                    b.Property<int>("HienThi");

                    b.Property<string>("HinhAnh")
                        .IsRequired();

                    b.Property<string>("KhoiLuongThung");

                    b.Property<string>("KichThuocMH");

                    b.Property<string>("KichThuocThung");

                    b.Property<string>("LoaiDanMay");

                    b.Property<string>("LoaiDauDia");

                    b.Property<int>("LoaiSanPham");

                    b.Property<string>("ManHinhCong");

                    b.Property<string>("MauSac");

                    b.Property<string>("MoTa");

                    b.Property<DateTime>("NgayTao");

                    b.Property<int>("NhaSanXuat");

                    b.Property<int>("SanPhamBanChay");

                    b.Property<string>("SmartTV");

                    b.Property<int>("SoLuong");

                    b.Property<string>("TanSoQuet");

                    b.Property<string>("TenSPCoDau")
                        .IsRequired();

                    b.Property<string>("TenSPKhongDau")
                        .IsRequired();

                    b.Property<string>("TrinhDuyetWeb");

                    b.Property<string>("XuatXu");

                    b.HasKey("ID");

                    b.ToTable("SanPham");
                });
        }
    }
}
