using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectricShop.Models;

namespace ElectricShop.Controllers
{
    public class SanPhamBanController : Controller
    {
        private readonly ElectricShopContext _context;

        public SanPhamBanController(ElectricShopContext context)
        {
            _context = context;    
        }
        public async Task<IActionResult> BanHang()
        {
            var dsLoaiSanPham = from lsp in _context.LoaiSanPham
                                select lsp;
            var loaiSanPhamDauTien = await (from lsp in dsLoaiSanPham
                                       select lsp).FirstOrDefaultAsync();
            var dsSanPhamTheoLoai = from sp in _context.SanPham
                                    where sp.LoaiSanPham == loaiSanPhamDauTien.ID
                                    select sp;
            ViewBag.dsLoaiSanPham =await dsLoaiSanPham.ToListAsync();
            ViewBag.dsSanPhamTheoLoai = await dsSanPhamTheoLoai.ToListAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanHang([Bind("ID,sanPhamID,khachHangID,maBaoHanh")] SanPhamBan SanPhamBan,string TenSP)
        {
            if (ModelState.IsValid)
            {
                var ngayTao = DateTime.Now;
                SanPhamBan.ngayBan = ngayTao;
                _context.Add(SanPhamBan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var khachhang = (from kh in _context.KhachHang
                            where kh.ID == SanPhamBan.khachHangID
                            select kh).FirstOrDefault();
            if(khachhang!=null)
            {
                ViewBag.tenKH = khachhang.HoTenKhachHang;
                ViewBag.soDT = khachhang.SoDienThoai;
                ViewBag.diaChi = khachhang.DiaChi;
            }
            ViewBag.tenSP = TenSP;
            ViewBag.sanPhamID = SanPhamBan.sanPhamID;
            var dsLoaiSanPham = from lsp in _context.LoaiSanPham
                                select lsp; 
            var loaiSanPhamDauTien = await (from lsp in dsLoaiSanPham
                                            select lsp).FirstOrDefaultAsync();
            var dsSanPhamTheoLoai = from sp in _context.SanPham
                                    where sp.LoaiSanPham == loaiSanPhamDauTien.ID
                                    select sp;
            ViewBag.dsLoaiSanPham = await dsLoaiSanPham.ToListAsync();
            ViewBag.dsSanPhamTheoLoai = await dsSanPhamTheoLoai.ToListAsync();
            return View(SanPhamBan);
        }
        //Ajax load danh sách sản phẩm theo loại
        public IActionResult LoadSanPhamTheoLoai(int idlsp)
        {
            var dsSanPhamTheoLoai = from sp in _context.SanPham
                                    where sp.LoaiSanPham == idlsp
                                    select sp;
            var rs = "";
            foreach(var sp in dsSanPhamTheoLoai)
            {
                rs = rs + "<option value= '"+sp.ID+"'>"+ sp.TenSPCoDau+ "</option>";
            }
            return Content(rs);
        }

        //Ajax load danh sách sản phẩm theo loại
        public IActionResult LoadThongTinSanPham(int idsp)
        {
            var sanPham = (from sp in _context.SanPham
                          where sp.ID == idsp
                          select sp).FirstOrDefault();
            var rs = "<div class='col-sm-5'>"+
                    "<img src='/"+sanPham.HinhAnh+ "' class='img-rounded'  width='150' height='150'>"+
            "</div>"+
            "<div class='col-sm-7'>"+
                "<p id='tensp'><b>"+sanPham.TenSPCoDau+"</b></p>"+
                "<p>Giá gốc :"+sanPham.GiaGoc+"</p>"+
                "<p>Giá giảm :" + sanPham.GiaGiam + "</p>" +
            "</div>" +
            "<input type='hidden' id ='masp' value='" + sanPham.ID + "' />";

            return Content(rs);
        }

        //Ajax load danh sách sản phẩm theo loại
        public IActionResult LoadThongTinSanPhamDauTien(int idlsp)
        {
            var dsSanPhamTheoLoai = (from sp in _context.SanPham
                                    where sp.LoaiSanPham == idlsp
                                    select sp).FirstOrDefault();
            var sanPham = (from sp in _context.SanPham
                           where sp.ID == dsSanPhamTheoLoai.ID
                           select sp).FirstOrDefault();
            var rs = "<div class='col-sm-5'>" +
                    "<img src='/" + sanPham.HinhAnh + "' class='img-rounded'  width='150' height='150'>" +
            "</div>" +
            "<div class='col-sm-7'>" +
                "<p id='tensp'><b>" + sanPham.TenSPCoDau + "</b></p>" +
                "<p>Giá gốc :" + sanPham.GiaGoc + "</p>" +
                "<p>Giá giảm :" + sanPham.GiaGiam + "</p>" +
            "</div>"+
            "<input type='hidden' id ='masp' value='"+sanPham.ID+"' />";
            return Content(rs);
        }

        // GET: SanPhamBan
        public async Task<IActionResult> Index()
        {
            return View(await _context.SanPhamBan.ToListAsync());
        }

        // GET: SanPhamBan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SanPhamBan = await _context.SanPhamBan
                .SingleOrDefaultAsync(m => m.ID == id);
            if (SanPhamBan == null)
            {
                return NotFound();
            }

            return View(SanPhamBan);
        }

        // GET: SanPhamBan/Create
        public IActionResult Create()
        {
            var dssp = from spb in _context.SanPhamBan
                       select spb;
            ViewBag.dsLoaiSanPham = dssp.ToList();
            return View();
        }
        // POST: SanPhamBan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,sanPhamID,khachHangID,maBaoHanh")] SanPhamBan SanPhamBan)
        {
            var dssp = from spb in _context.SanPhamBan
                       select spb;
            ViewBag.dsLoaiSanPham = dssp.ToList();
            if (ModelState.IsValid)
            {
                _context.Add(SanPhamBan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(SanPhamBan);
        }

        // GET: SanPhamBan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SanPhamBan = await _context.SanPhamBan.SingleOrDefaultAsync(m => m.ID == id);
            if (SanPhamBan == null)
            {
                return NotFound();
            }
            return View(SanPhamBan);
        }

        // POST: SanPhamBan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,sanPhamID,maBaoHanh,ngayBan")] SanPhamBan SanPhamBan)
        {
            if (id != SanPhamBan.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(SanPhamBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamBanExists(SanPhamBan.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(SanPhamBan);
        }

        // GET: SanPhamBan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SanPhamBan = await _context.SanPhamBan
                .SingleOrDefaultAsync(m => m.ID == id);
            if (SanPhamBan == null)
            {
                return NotFound();
            }

            return View(SanPhamBan);
        }

        // POST: SanPhamBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var SanPhamBan = await _context.SanPhamBan.SingleOrDefaultAsync(m => m.ID == id);
            _context.SanPhamBan.Remove(SanPhamBan);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SanPhamBanExists(int id)
        {
            return _context.SanPhamBan.Any(e => e.ID == id);
        }
    }
}
