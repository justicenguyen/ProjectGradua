using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class DonHang
    {
        public int ID { get; set; }
        [DisplayName("Họ tên khách hàng *")]
        [Required(ErrorMessage = "Trường này là bắt buộc !")]
        public string TenKhachHang{get;set;}
        [DisplayName("Họ tên khách hàng *")]
        [Required(ErrorMessage = "Số điện thoại liên lạc !")]
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        [DisplayName("Địa chỉ nhận hàng*")]
        [Required(ErrorMessage = "Trường này là bắt buộc !")]
        public string DiaChi { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int? TongTien { get; set; }
        public bool DaDuyet { get; set; }
        public bool DaGiao { get; set; }
    }
}
