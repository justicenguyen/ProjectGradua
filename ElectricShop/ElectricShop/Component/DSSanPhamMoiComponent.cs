using ElectricShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Component
{
    public class DSSanPhamMoiComponent:ViewComponent
    {
        private readonly ElectricShopContext _context;

        public DSSanPhamMoiComponent(ElectricShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dsspmoi = (from lsp in _context.SanPham
                           orderby lsp.ID descending
                           select lsp).Skip(0).Take(6);
            return View(await dsspmoi.ToListAsync());
        }
    }
}
