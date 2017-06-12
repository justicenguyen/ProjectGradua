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
    public class DonHangController : Controller
    {
        private readonly ElectricShopContext _context;

        public DonHangController(ElectricShopContext context)
        {
            _context = context;    
        }

        // GET: DonHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.DonHang.ToListAsync());
        }
        public async Task<IActionResult> ChiTietDonHang(int? id)
        {
            var donHang = await _context.DonHang
                .SingleOrDefaultAsync(m => m.ID == id);
            var dsChiTiet = from ct in _context.ChiDietDonHang
                            where ct.DonHangID == id
                            select ct;
            var dsMatHang = new List<MatHang>();
            foreach(var ct in dsChiTiet)
            {
                var sanPham = await _context.SanPham.SingleOrDefaultAsync(sp => sp.ID == ct.SanPhamID);
                var mh = new MatHang();
                mh.sanPham = sanPham;
                mh.soLuong = ct.SoLuong;
                mh.tongTien = ct.TongTien;
                dsMatHang.Add(mh);
            }
            var duLieuChiTiet = new DuLieuChiTietDonHang();
            duLieuChiTiet.donHang = donHang;
            duLieuChiTiet.chiTietDonHang = dsMatHang;
            return View(duLieuChiTiet);
        }

        // GET: DonHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang
                .SingleOrDefaultAsync(m => m.ID == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // GET: DonHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DonHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenKhachHang,SoDienThoai,Email,DiaChi,TongTien")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(donHang);
        }

        // GET: DonHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang.SingleOrDefaultAsync(m => m.ID == id);
            if (donHang == null)
            {
                return NotFound();
            }
            return View(donHang);
        }

        // POST: DonHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TenKhachHang,SoDienThoai,Email,DiaChi,TongTien")] DonHang donHang)
        {
            if (id != donHang.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.ID))
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
            return View(donHang);
        }

        // GET: DonHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var donHang = await _context.DonHang
                .SingleOrDefaultAsync(m => m.ID == id);
            if (donHang == null)
            {
                return NotFound();
            }
            var dsChiTiet = from ct in _context.ChiDietDonHang
                            where ct.DonHangID == id
                            select ct;
            foreach(var mh in dsChiTiet )
            {
               // var matHang = await _context.ChiDietDonHang.SingleOrDefaultAsync(m => m.DonHangID == mh.DonHangID);
                _context.ChiDietDonHang.Remove(mh);
            }
            _context.DonHang.Remove(donHang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ThayDoiDaDuyet(int? idDH)
        {
            if (idDH == null)
            {
                return NotFound();
            }
            var donHang = await _context.DonHang
                .SingleOrDefaultAsync(m => m.ID == idDH);
            donHang.DaDuyet = !donHang.DaDuyet;
            _context.Update(donHang);
            await _context.SaveChangesAsync();
            return Content("Thay đổi thành công");
        }
        public async Task<IActionResult> ThayDoiDaGiao(int? idDH)
        {
            if (idDH == null)
            {
                return NotFound();
            }
            var donHang = await _context.DonHang
                .SingleOrDefaultAsync(m => m.ID == idDH);
            donHang.DaGiao = !donHang.DaGiao;
            _context.Update(donHang);
            await _context.SaveChangesAsync();
            return Content("Thay đổi thành công");
        }

        // POST: DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donHang = await _context.DonHang.SingleOrDefaultAsync(m => m.ID == id);
            _context.DonHang.Remove(donHang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DonHangExists(int id)
        {
            return _context.DonHang.Any(e => e.ID == id);
        }
    }
}
