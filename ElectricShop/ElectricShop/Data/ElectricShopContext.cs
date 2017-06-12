using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElectricShop.Models;

namespace ElectricShop.Models
{
    public class ElectricShopContext : DbContext
    {
        public ElectricShopContext (DbContextOptions<ElectricShopContext> options)
            : base(options)
        {
        }

        public DbSet<ElectricShop.Models.LoaiSanPham> LoaiSanPham { get; set; }

        public DbSet<ElectricShop.Models.SanPham> SanPham { get; set; }

        public DbSet<ElectricShop.Models.NhaSanXuat> NhaSanXuat { get; set; }

        public DbSet<ElectricShop.Models.KhuyenMai> KhuyenMai { get; set; }

        public DbSet<ElectricShop.Models.BinhLuan> BinhLuan { get; set; }

        public DbSet<ElectricShop.Models.DonHang> DonHang { get; set; }

        public DbSet<ElectricShop.Models.ChiDietDonHang> ChiDietDonHang { get; set; }

        public DbSet<ElectricShop.Models.SanPhamBan> SanPhamBan { get; set; }

        public DbSet<ElectricShop.Models.KhachHang> KhachHang { get; set; }
    }
}
