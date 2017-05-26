
using ElectricShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Component
{
    public class MenuLSPComponent: Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly ElectricShopContext _context;

        public MenuLSPComponent(ElectricShopContext context)
        {
            _context = context;
        }
        public async Task<Microsoft.AspNetCore.Mvc.IViewComponentResult> InvokeAsync()
        {
            var dsloaisp = from lsp in _context.LoaiSanPham
                           select lsp;
            return View( await dsloaisp.ToListAsync());
        }
    }
}
