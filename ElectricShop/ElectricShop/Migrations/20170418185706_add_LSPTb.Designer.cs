using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ElectricShop.Models;

namespace ElectricShop.Migrations
{
    [DbContext(typeof(ElectricShopContext))]
    [Migration("20170418185706_add_LSPTb")]
    partial class add_LSPTb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
        }
    }
}
