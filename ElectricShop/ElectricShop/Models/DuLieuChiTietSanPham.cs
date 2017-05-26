using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class DuLieuChiTietSanPham
    {
        public SanPham sanpham;
        public List<SanPham> dssanphamlienquan;
        public string HangSanXuat;
        public List<BinhLuan> dsbinhluan;
        [Required(ErrorMessage = "Vui lòng nhập Họ và Tên")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Vuu lòng nhập Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SoDienThoai { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập nội dung bình luận")]
        public string NoiDung { get; set; }
    }
}
