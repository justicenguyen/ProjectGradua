using ElectricShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricShop.Component
{
    public class SliderAdvComponent: ViewComponent
    {
        private readonly ElectricShopContext _context;

        public SliderAdvComponent(ElectricShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dsctkm = from ctkm in _context.KhuyenMai
                         where ctkm.HienThi==1
                         select ctkm;
            return View(await dsctkm.ToListAsync());
        }
    }
}
