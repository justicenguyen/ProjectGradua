using ElectricShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Component
{
    public class Siderbar_SPBCComponent:ViewComponent
    {
        private readonly ElectricShopContext _context;

        public Siderbar_SPBCComponent(ElectricShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dsLoaiSanPham = from lsp in _context.LoaiSanPham
                           select lsp;
            var dsSanPhamBanChay = from sp in _context.SanPham
                                   where sp.SanPhamBanChay == true
                                   select sp;
            var siderBar_SPBC = new SideBar_SPBCModel();
            siderBar_SPBC.dsLoaiSanPham = await dsLoaiSanPham.ToListAsync();
            siderBar_SPBC.dsSanPhamBanChay = await dsSanPhamBanChay.ToListAsync();
            return View(siderBar_SPBC);
        }
    }
}
