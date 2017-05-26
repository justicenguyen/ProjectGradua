using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Models
{
    public class SanPham
    {
        // Thong tin chung
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm có dấu !")]
        public string TenSPCoDau { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm có không dấu !")]
        public string TenSPKhongDau { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập giá sản phẩm !")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int? Gia { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int GiaGiam { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn ảnh đại diện cho sản phẩm !")]
        public string HinhAnh { get; set;}
        public int LoaiSanPham { get; set; }
        public string MauSac { get; set; }
        public int NhaSanXuat { get; set; }
        public string XuatXu { get; set; }
        public int BaoHanh { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayTao { get; set; }
        public int HienThi { get; set; }
        public int SanPhamBanChay { get; set; }
        public int SoLuong { get; set; }
        public string KichThuocThung { get; set; }
        public string KhoiLuongThung { get; set; }
        public string MoTa { get; set; }
        // TIVI
        public string KichThuocMH { get; set; }
        public string DoPhanGiai { get; set; }
        public string ManHinhCong { get; set; }
        public string BoXuLy { get; set; }
        public string SmartTV { get; set; }
        public string TanSoQuet { get; set; }
        public string CongSuatLoa { get; set; }
        public string CongWiFi { get; set; }
        public string CongInternet { get; set; }
        public string CongHDMI { get; set; }
        public string CongUSB { get; set; }
        public string ChiaSeThongMinh { get; set; }
        public string HeDeHanh { get; set; }
        public string TrinhDuyetWeb { get; set; }
        // Dan may nghe nhac
        public string LoaiDanMay { get; set; }
        public string LoaiDauDia { get; set; }
        
    }
}
