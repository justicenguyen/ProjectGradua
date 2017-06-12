using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class KhachHang
    {
        public int ID { get; set; }
        [Required]
        public string HoTenKhachHang { get; set; }
        [Required]
        public string SoDienThoai { get; set; }
        [Required]
        public string DiaChi { get; set;}
    }
}
