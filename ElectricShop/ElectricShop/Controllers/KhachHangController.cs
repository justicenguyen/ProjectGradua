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
    public class KhachHangController : Controller
    {
        private readonly ElectricShopContext _context;

        public KhachHangController(ElectricShopContext context)
        {
            _context = context;    
        }

        // GET: KhachHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.KhachHang.ToListAsync());
        }
        //Ajax thêm khách hàng
        public IActionResult ThemKhachHang(KhachHang khachHang)
        {
            string khachHangId="0";
            if (ModelState.IsValid)
            {
                var khachhang = (from kh in _context.KhachHang
                                 where kh.SoDienThoai == khachHang.SoDienThoai
                                 select kh).FirstOrDefault();
                
                if (khachhang == null)
                {
                    _context.Add(khachHang);
                    _context.SaveChanges();
                    var khachhanghientai = (from kh in _context.KhachHang
                                            orderby kh.ID descending
                                            select kh).FirstOrDefault();
                    khachHangId = Convert.ToString(khachhanghientai.ID);
                }
                else
                {
                    khachHangId = Convert.ToString(khachhang.ID);
                }

            }
            return Content(khachHangId);
        }

        // GET: KhachHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang
                .SingleOrDefaultAsync(m => m.ID == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // GET: KhachHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HoTenKhachHang,SoDienThoai,DiaChi")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        // GET: KhachHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang.SingleOrDefaultAsync(m => m.ID == id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HoTenKhachHang,SoDienThoai,DiaChi")] KhachHang khachHang)
        {
            if (id != khachHang.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.ID))
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
            return View(khachHang);
        }

        // GET: KhachHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang
                .SingleOrDefaultAsync(m => m.ID == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _context.KhachHang.SingleOrDefaultAsync(m => m.ID == id);
            _context.KhachHang.Remove(khachHang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool KhachHangExists(int id)
        {
            return _context.KhachHang.Any(e => e.ID == id);
        }
    }
}
