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
    public class BinhLuanController : Controller
    {
        private readonly ElectricShopContext _context;

        public BinhLuanController(ElectricShopContext context)
        {
            _context = context;    
        }

        // GET: BinhLuan
        public async Task<IActionResult> Index()
        {
            return View(await _context.BinhLuan.ToListAsync());
        }

        // GET: BinhLuan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuan = await _context.BinhLuan
                .SingleOrDefaultAsync(m => m.ID == id);
            if (binhLuan == null)
            {
                return NotFound();
            }

            return View(binhLuan);
        }

        // GET: BinhLuan/Create
        public IActionResult Create()
        {
            return RedirectToAction("TrangChu", "TrangChu");
           // return View();
        }

        // POST: BinhLuan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HoTen,Email,SoDienThoai,NoiDung")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(binhLuan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(binhLuan);
        }

        // GET: BinhLuan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuan = await _context.BinhLuan.SingleOrDefaultAsync(m => m.ID == id);
            if (binhLuan == null)
            {
                return NotFound();
            }
            return View(binhLuan);
        }

        // POST: BinhLuan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HoTen,Email,SoDienThoai,NoiDung")] BinhLuan binhLuan)
        {
            if (id != binhLuan.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(binhLuan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BinhLuanExists(binhLuan.ID))
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
            return View(binhLuan);
        }

        // GET: BinhLuan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuan = await _context.BinhLuan
                .SingleOrDefaultAsync(m => m.ID == id);
            if (binhLuan == null)
            {
                return NotFound();
            }

            return View(binhLuan);
        }

        // POST: BinhLuan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var binhLuan = await _context.BinhLuan.SingleOrDefaultAsync(m => m.ID == id);
            _context.BinhLuan.Remove(binhLuan);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BinhLuanExists(int id)
        {
            return _context.BinhLuan.Any(e => e.ID == id);
        }
    }
}
