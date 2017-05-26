using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class SanPhamGioHang
    {
        public int idSP { get; set; }
        public string TenSP { get; set; }
        public string HinhAnh { get; set; }
        public int? Gia { get; set; }
        public int SoLuong { get; set; }
        public int? TongTien { get; set; }
    }
}
