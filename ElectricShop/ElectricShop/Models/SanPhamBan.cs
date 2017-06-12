using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class SanPhamBan
    {
        public int ID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Bạn chưa chọn sản phẩm")]
        [Required(ErrorMessage = "bạn chưa chọn sản phẩm !")]
        public int sanPhamID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Bạn chưa nhập đầy đủ thông tin khách hàng")]
        public int khachHangID { get; set; }
        [Required(ErrorMessage = "bạn chưa nhập mã bảo hành !")]
        public string maBaoHanh { get; set; }
        public DateTime ngayBan { get; set; }
    }
}
