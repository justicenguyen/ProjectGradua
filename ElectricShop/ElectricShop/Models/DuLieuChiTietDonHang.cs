using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class DuLieuChiTietDonHang
    {
        public DonHang donHang { get; set; }
        public List<MatHang> chiTietDonHang { get; set; }
    }
}
