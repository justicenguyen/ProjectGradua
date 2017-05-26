using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class DonHang
    {
        public int ID { get; set; }
        public string TenKhachHang{get;set;}
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public int? TongTien { get; set; }
    }
}
