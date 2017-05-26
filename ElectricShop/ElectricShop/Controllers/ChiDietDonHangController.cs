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
    public class ChiDietDonHangController : Controller
    {
        private readonly ElectricShopContext _context;

        public ChiDietDonHangController(ElectricShopContext context)
        {
            _context = context;    
        }

        // GET: ChiDietDonHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChiDietDonHang.ToListAsync());
        }

        // GET: ChiDietDonHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiDietDonHang = await _context.ChiDietDonHang
                .SingleOrDefaultAsync(m => m.ID == id);
            if (chiDietDonHang == null)
            {
                return NotFound();
            }

            return View(chiDietDonHang);
        }

        // GET: ChiDietDonHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChiDietDonHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SanPhamID,DonHangID,Gia,SoLuong,TongTien")] ChiDietDonHang chiDietDonHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiDietDonHang);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chiDietDonHang);
        }

        // GET: ChiDietDonHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiDietDonHang = await _context.ChiDietDonHang.SingleOrDefaultAsync(m => m.ID == id);
            if (chiDietDonHang == null)
            {
                return NotFound();
            }
            return View(chiDietDonHang);
        }

        // POST: ChiDietDonHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SanPhamID,DonHangID,Gia,SoLuong,TongTien")] ChiDietDonHang chiDietDonHang)
        {
            if (id != chiDietDonHang.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiDietDonHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiDietDonHangExists(chiDietDonHang.ID))
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
            return View(chiDietDonHang);
        }

        // GET: ChiDietDonHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiDietDonHang = await _context.ChiDietDonHang
                .SingleOrDefaultAsync(m => m.ID == id);
            if (chiDietDonHang == null)
            {
                return NotFound();
            }

            return View(chiDietDonHang);
        }

        // POST: ChiDietDonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiDietDonHang = await _context.ChiDietDonHang.SingleOrDefaultAsync(m => m.ID == id);
            _context.ChiDietDonHang.Remove(chiDietDonHang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ChiDietDonHangExists(int id)
        {
            return _context.ChiDietDonHang.Any(e => e.ID == id);
        }
    }
}
