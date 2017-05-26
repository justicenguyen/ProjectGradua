using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class DuLieuTrangChu
    {
        public SanPham sanpham;
        public LoaiSanPham loaisanpham;
        public List<LoaiSanPham> dsloaisanpham;
        public List<SanPham> dssanphamhienthi;
        public List<SanPham> dssanphammoinhat;
        public List<SanPham> dssanphambanchay;
        public List<KhuyenMai> dskhuyenmai;
    }
}
