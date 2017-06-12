using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using ElectricShop.Models;
using Microsoft.EntityFrameworkCore;
//using System.Web.Script.Seriallization;
using Newtonsoft.Json;

namespace ElectricShop.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ElectricShopContext _context;
        public GioHangController(ElectricShopContext context)
        {
            _context = context;
        }
        private const string GioHangSession = "GioHangSession";
        public IActionResult GioHang()
        {
           
            var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            if (cart == null)
            {
                cart = HttpContext.Request.Cookies.GetKookies<List<SanPhamGioHang>>(GioHangSession);
            }
            var list = new List<SanPhamGioHang>();
            if (cart != null)
            {
                list = (List<SanPhamGioHang>)cart;
            }
            return View(list);
        }

        public IActionResult GioHang1()
        {

            var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            if (cart == null)
            {
                cart = HttpContext.Request.Cookies.GetKookies<List<SanPhamGioHang>>(GioHangSession);
            }
            var list = new List<SanPhamGioHang>();
            if (cart != null)
            {
                list = (List<SanPhamGioHang>)cart;
            }
            return View(list);
        }

        public async Task<IActionResult> ThemGioHang(int idSP, int soLuong)
        {

            var sanPham = await _context.SanPham
               .SingleOrDefaultAsync(m => m.ID == idSP);

            var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            if (cart == null)
            {
                cart = HttpContext.Request.Cookies.GetKookies<List<SanPhamGioHang>>(GioHangSession);
            }
            if (cart != null)
            {
                var list = (List<SanPhamGioHang>)cart;
                if (list.Exists(x => x.idSP == idSP))
                {
                    foreach (var item in list)
                    {
                        if (item.idSP == idSP)
                        {
                            item.SoLuong += soLuong;
                            int? GiaThem = soLuong * item.Gia;
                            item.TongTien = item.TongTien + GiaThem;
                        }
                    }
                }
                else
                {
                    var item = new SanPhamGioHang();
                    item.idSP = sanPham.ID;
                    item.TenSP = sanPham.TenSPCoDau;
                    item.Gia = sanPham.GiaGiam;
                    item.HinhAnh = sanPham.HinhAnh;
                    item.SoLuong = soLuong;
                    item.TongTien = soLuong * sanPham.GiaGiam;
                    list.Add(item);
                }
                //Gan vao session
                HttpContext.Session.SetSession(GioHangSession, list);
                HttpContext.Response.Cookies.SetKookies(GioHangSession, list);

            }
            else
            {
                var item = new SanPhamGioHang();
                item.idSP = sanPham.ID;
                item.TenSP = sanPham.TenSPCoDau;
                item.Gia = sanPham.GiaGiam;
                item.HinhAnh = sanPham.HinhAnh;
                item.SoLuong = soLuong;
                item.TongTien = soLuong * sanPham.GiaGiam;
                var list = new List<SanPhamGioHang>();
                list.Add(item);
                //Gan vao session
                HttpContext.Session.SetSession(GioHangSession, list);
                HttpContext.Response.Cookies.SetKookies(GioHangSession, list);
            }
            return Content("The vao gio hang thanh cong", "text/plain");
        }

        // Ajax cập nhật giỏ hàng
        public async Task<IActionResult> SuaGioHang(int idSP, int soLuong)
        {
            var sanPham = await _context.SanPham
               .SingleOrDefaultAsync(m => m.ID == idSP);
            var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            if (cart == null)
            {
                cart = HttpContext.Request.Cookies.GetKookies<List<SanPhamGioHang>>(GioHangSession);
            }
            if (cart != null)
            {
                var list = (List<SanPhamGioHang>)cart;
                if (list.Exists(x => x.idSP == idSP))
                {
                    foreach (var item in list)
                    {
                        if (item.idSP == idSP)
                        {
                            item.SoLuong += soLuong;
                            int? GiaThem = soLuong * item.Gia;
                            item.TongTien = item.TongTien + GiaThem;
                        }
                    }
                }
                else
                {
                    var item = new SanPhamGioHang();
                    item.idSP = sanPham.ID;
                    item.TenSP = sanPham.TenSPCoDau;
                    item.Gia = sanPham.GiaGiam;
                    item.HinhAnh = sanPham.HinhAnh;
                    item.SoLuong = soLuong;
                    item.TongTien = soLuong * sanPham.GiaGiam;
                    list.Add(item);
                }
                //Gan vao session
                HttpContext.Session.SetSession(GioHangSession, list);
                HttpContext.Response.Cookies.SetKookies(GioHangSession, list);

            }
            else
            {
                var item = new SanPhamGioHang();
                item.idSP = sanPham.ID;
                item.TenSP = sanPham.TenSPCoDau;
                item.Gia = sanPham.GiaGiam;
                item.SoLuong = soLuong;
                item.HinhAnh = sanPham.HinhAnh;
                item.TongTien = soLuong * sanPham.GiaGiam;
                var list = new List<SanPhamGioHang>();
                list.Add(item);
                //Gan vao session
                HttpContext.Session.SetSession(GioHangSession, list);
                HttpContext.Response.Cookies.SetKookies(GioHangSession, list);
            }
            return Content("Sửa thành công", "text/plain");
        }

        // Xóa sản phẩm trong giỏ hàng
        public IActionResult XoaGioHang(int idSP)
        {
            var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            cart.RemoveAll(x => x.idSP == idSP);
            HttpContext.Session.SetSession(GioHangSession, cart);
            HttpContext.Response.Cookies.SetKookies(GioHangSession, cart);
            return Content("Bo san pham khoi gio hang thanh cong", "text/plain");
        }

        //Đặt hàng
        public async Task<IActionResult> DatHang([Bind("TenKhachHang,SoDienThoai,Email,DiaChi")] DonHang donHang)
        {
            //DonHang dh = new DonHang { TenKhachHang = donHang.TenKhachHang, SoDienThoai = donHang.SoDienThoai, Email = donHang.Email, DiaChi = donHang.DiaChi, TongTien = 1111 };
            //_context.DonHang.Add(dh);
            if (ModelState.IsValid)
            {

                var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
                int? TongTien = 0;
                foreach(var tt in cart)
                {
                    TongTien = TongTien + tt.TongTien;
                }
                donHang.TongTien = TongTien;
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                var dh = (from d in _context.DonHang
                          orderby d.ID descending
                          select d).FirstOrDefault();
               
                foreach(var sp in cart)
                {
                    ChiDietDonHang ctdh = new ChiDietDonHang {SanPhamID=sp.idSP,DonHangID= dh.ID,Gia=sp.Gia,SoLuong=sp.SoLuong,TongTien=sp.TongTien};
                    _context.ChiDietDonHang.Add(ctdh);
                }
                await _context.SaveChangesAsync();
                HttpContext.Session.SetSession(GioHangSession, null);
                HttpContext.Response.Cookies.SetKookies(GioHangSession, null);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
    }
}