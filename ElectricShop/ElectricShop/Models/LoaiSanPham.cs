using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class LoaiSanPham
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Tên loại sản phẩm có dấu *")]
        [Required(ErrorMessage = "Trường này là bắt buộc !")]
        public string TenLoaiSPCoDau { get; set; }
        [DisplayName("Tên loại sản phẩm không dấu *")]
        [Required(ErrorMessage = "Trường này bắt buộc !")]
        public string TenLoaiSPKhongDau { get; set; }
    }
}
