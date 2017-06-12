using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class MatHang
    {
        public SanPham sanPham { get; set; }
        public int? soLuong { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int? tongTien { get; set; }
    }
}
