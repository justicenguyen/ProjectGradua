using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class SanPhamGioHang
    {
        public int idSP { get; set; }
        public string TenSP { get; set; }
        public string HinhAnh { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int? Gia { get; set; }
        public int SoLuong { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int? TongTien { get; set; }
    }
}
