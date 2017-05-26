using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class LoaiSanPham
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên loại sản phẩm có dấu !")]
        public string TenLoaiSPCoDau { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên loại sản phẩm không dấu !")]
        public string TenLoaiSPKhongDau { get; set; }
    }
}
