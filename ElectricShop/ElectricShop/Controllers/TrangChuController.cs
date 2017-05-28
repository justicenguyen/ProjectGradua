﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElectricShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricShop.Controllers
{
    public class TrangChuController : Controller
    {
        private readonly ElectricShopContext _context;

        public TrangChuController(ElectricShopContext context)
        {
            _context = context;
        }
        public IActionResult VD()
        {
            return View();
        }
        public async Task<IActionResult> TrangChu()
        {
            Microsoft.AspNetCore.Http.CookieOptions options = new Microsoft.AspNetCore.Http.CookieOptions();
            options.Expires = DateTime.Now.AddSeconds(30);
           
            var dssanphamhienthi = from spht in _context.SanPham
                                   where spht.HienThi == true
                                   select spht;
            var dssanphambanchay = from spbc in _context.SanPham
                                   where spbc.SanPhamBanChay==true
                                   select spbc;
            var dssanphammoinhat = (from spmn in _context.SanPham
                                    orderby spmn.NgayTao
                                    select spmn).Skip(0).Take(5);
            var dsloaisanpham = from lsp in _context.LoaiSanPham
                                select lsp;
            var dskhuyenmai = from km in _context.KhuyenMai
                              where km.HienThi==1
                              select km;
            ViewData["menu"] = dsloaisanpham;
            ViewData["dsloaisanpham"] = dsloaisanpham;
            var dsdulieu = new DuLieuTrangChu();
            dsdulieu.dssanphamhienthi = await dssanphamhienthi.ToListAsync();
            dsdulieu.dsloaisanpham = await dsloaisanpham.ToListAsync();
            dsdulieu.dssanphambanchay = await dssanphambanchay.ToListAsync();
            dsdulieu.dskhuyenmai = await dskhuyenmai.ToListAsync();
            return View(dsdulieu);
        }




        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
