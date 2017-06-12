using ElectricShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Component
{
    public class BoLocComponent:ViewComponent
    {
        private readonly ElectricShopContext _context;

        public BoLocComponent(ElectricShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int a, int b)
        {
            var dsHangSanXuat = from nsx in _context.NhaSanXuat
                                where nsx.ID > a  
                              select nsx;
            return View(await dsHangSanXuat.ToListAsync());
        }

    }
}
