using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class KhuyenMai
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Bạn chưa nhập tiêu đề khuyến mãi !")]
        public string TieuDeKhuyenMai { get; set; }
        [Required(ErrorMessage ="Bạn chưa chọn hình ảnh !")]
        public string HinhAnh { get; set; }
        public string NoiDung { get; set; }
        public int HienThi { get; set; }
    }
}
