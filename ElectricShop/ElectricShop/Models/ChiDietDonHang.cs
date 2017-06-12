using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class ChiDietDonHang
    {
        public int ID { get; set; }
        public int SanPhamID { get; set; }
        public int DonHangID { get; set; }
        public int? Gia { get; set; }
        public int? SoLuong { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int? TongTien { get; set; }

    }
}
