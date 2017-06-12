using ElectricShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Component
{
    public class DSSanPhamKhuyenMaiComponent:ViewComponent
    {
        private readonly ElectricShopContext _context;

        public DSSanPhamKhuyenMaiComponent(ElectricShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int idLSP)
        {
            var dsSanPhamKhuyenMai = from sp in _context.SanPham
                                where sp.GiaGiam >0 && sp.LoaiSanPham==idLSP
                                select sp;
            return View(await dsSanPhamKhuyenMai.ToListAsync());
        }
    }
}
