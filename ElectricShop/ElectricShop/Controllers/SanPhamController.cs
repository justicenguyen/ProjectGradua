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
    public class SanPhamController : Controller
    {
        private readonly ElectricShopContext _context;

        public SanPhamController(ElectricShopContext context)
        {
            _context = context;    
        }
        public IActionResult a()
        {
            return View();
            // return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Tao([Bind("ID,HoTen,Email,SoDienThoai,NoiDung")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(binhLuan);
                await _context.SaveChangesAsync();
                return RedirectToAction("TrangChu","TrangChu");
            }
            return View("ChiTiet",binhLuan);
        }

        public async Task<IActionResult> ChiTiet(string id)
        {
            var sanpham = await _context.SanPham
                .SingleOrDefaultAsync(m => m.TenSPKhongDau== id);
            var dsloaisanpham = from lsp in _context.LoaiSanPham
                                select lsp;
            ViewData["menu"] = dsloaisanpham;
            var nhasanxuat = await _context.NhaSanXuat
               .SingleOrDefaultAsync(m => m.ID == sanpham.NhaSanXuat);
            string HangSanXuat = nhasanxuat.TenNSXCoDau;
            var dssanphamlienquan = (from splq in _context.SanPham
                                     where splq.LoaiSanPham==sanpham.LoaiSanPham && splq.TenSPKhongDau!=id 
                                     select splq).Take(6);
            var dsbinhluan = from bl in _context.BinhLuan
                             where bl.MaSanPham == sanpham.ID
                             orderby bl.ThoiGian descending
                             select bl;

            var chitietsanpham = new DuLieuChiTietSanPham();
            chitietsanpham.dssanphamlienquan = await dssanphamlienquan.ToListAsync();
            chitietsanpham.sanpham = sanpham;
            chitietsanpham.HangSanXuat = HangSanXuat;
            chitietsanpham.dsbinhluan = await dsbinhluan.ToListAsync();
            return View(chitietsanpham);
        }
        //Ajax thêm bình luận
        public async Task<ActionResult> BinhLuan(BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(binhLuan);
                await _context.SaveChangesAsync();
                string s = "<p><span style='font-size:17px;'><b>" + binhLuan.HoTen+" : </b></span>"+binhLuan.NoiDung+ "</p> <p><a href='#'>Thích </a> &emsp; <a href='#'> Trả lời </a>&emsp;"+   binhLuan.ThoiGian+"</p><hr/>";
                return Content(s, "text/plain");
            }
            return View("ChiTiet", binhLuan);
        }
        
        //Danh sách sản phẩm theo loại
        public async Task<IActionResult> DanhSachSanPhamTheoLoai(string id,string hang,string gia)
        {
            // Loại sản phẩm tương ứng
            var loaisanpham = await _context.LoaiSanPham
                .SingleOrDefaultAsync(m => m.TenLoaiSPKhongDau == id);
            //Danh sách sản phẩm ứng với loại trên
            var dssanpham = from sp in _context.SanPham
                            where sp.LoaiSanPham == loaisanpham.ID
                            select sp;
            //Lấy các nhà sản xuất tương ứng của loại sản phẩm này hiện co ở cửa hàng
            var dsnsx = from key in dssanpham
                        group key by key.NhaSanXuat;
            var dsnhasanxuat = from nsx in _context.NhaSanXuat
                               join k in dsnsx on nsx.ID equals k.Key
                               select nsx;
            // Lọc sản phẩm theo hãng sãn xuất
            ViewData["hang"] = "";
            if (!String.IsNullOrEmpty(hang))
            {
                var nhasanxuat = await _context.NhaSanXuat
                    .SingleOrDefaultAsync(m => m.TenNSXKhongDau == hang);
                dssanpham = dssanpham.Where(h => h.NhaSanXuat.Equals(nhasanxuat.ID));
                ViewData["hang"] = hang;
            }

            //Lọc sản phẩm theo giá
            ViewData["gia"] = "";
            if (!String.IsNullOrEmpty(gia))
            {
                if(gia.Equals("duoi-5-trieu"))
                {
                    dssanpham = dssanpham.Where(g => g.GiaGiam<5000000);
                    ViewData["gia"] = "duoi-5-trieu";
                }
                else
                {
                    if (gia.Equals("tu-5-7-trieu"))
                    {
                        dssanpham = dssanpham.Where(g => (g.GiaGiam >= 5000000&&g.GiaGiam < 7000000));
                        ViewData["gia"] = "tu-5-7-trieu";
                    }
                    else
                    {
                        if (gia.Equals("tu-7-10-trieu"))
                        {
                            dssanpham = dssanpham.Where(g => (g.GiaGiam >= 7000000 && g.GiaGiam < 10000000));
                            ViewData["gia"] = "tu-7-10-trieu";
                        }
                        else
                        {
                            dssanpham = dssanpham.Where(g => g.GiaGiam > 10000000);
                            ViewData["gia"] = "tren-10-trieu";
                        }
                    }
                }
            }

            //Dữ liệu truyền sang View
            var dssanphamtheoloai = new DuLieuDanhSachSanPhamTheoLoai();
            dssanphamtheoloai.dssanphamtheoloai = await dssanpham.ToListAsync();
            dssanphamtheoloai.loaisanpham = loaisanpham;
            dssanphamtheoloai.dsnhasanxuat = await dsnhasanxuat.ToListAsync();
            return View(dssanphamtheoloai);
        }

        //Danh sách sản phẩm tìm kiếm theo tên sản phẩm
        public async Task<IActionResult> DanhSachSanPhamTimKiem(string id,string hang)
        {
            //Danh sách sản phẩm lọc ra theo từ khóa
            var dssanpham = from sp in _context.SanPham
                            where sp.TenSPCoDau.Contains(id)
                            select sp;
            //Dữ liệu truyền sang View
            var dssanphamtheoloai = new DanhSachSanPhamTimKiem();
            dssanphamtheoloai.dssanphamtimkiem = await dssanpham.ToListAsync();
            dssanphamtheoloai.tukhoa = id;
            ViewData["tukhoa"] = id;
            return View(dssanphamtheoloai);
        }

        // GET: SanPham
        public async Task<IActionResult> Index()
        {
            return View(await _context.SanPham.ToListAsync());
        }
        public async Task<IActionResult> TrangChu()
        {
            return View(await _context.SanPham.ToListAsync());
        }

        // GET: SanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .SingleOrDefaultAsync(m => m.ID == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPham/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenSPCoDau,TenSPKhongDau,LoaiSanPham,Gia,HinhAnh,MauSac,NhaSanXuat,XuatXu,BaoHanh,NgayTao,HienThi,SanPhamBanChay,SoLuong,KichThuocThung,KhoiLuongThung,KichThuocMH,DoPhanGiai,ManHinhCong,BoXuLy,SmartTV,TanSoQuet,CongSuatLoa,CongWiFi,CongInternet,CongHDMI,CongUSB,ChiaSeThongMinh,HeDeHanh,TrinhDuyetWeb,LoaiDanMay,LoaiDauDia")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }

        // GET: SanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.SingleOrDefaultAsync(m => m.ID == id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        // POST: SanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TenSPCoDau,TenSPKhongDau,Gia,HinhAnh,MauSac,NhaSanXuat,XuatXu,BaoHanh,NgayTao,HienThi,SanPhamBanChay,SoLuong,KichThuocThung,KhoiLuongThung,KichThuocMH,DoPhanGiai,ManHinhCong,BoXuLy,SmartTV,TanSoQuet,CongSuatLoa,CongWiFi,CongInternet,CongHDMI,CongUSB,ChiaSeThongMinh,HeDeHanh,TrinhDuyetWeb,LoaiDanMay,LoaiDauDia")] SanPham sanPham)
        {
            if (id != sanPham.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.ID))
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
            return View(sanPham);
        }

        // GET: SanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .SingleOrDefaultAsync(m => m.ID == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPham.SingleOrDefaultAsync(m => m.ID == id);
            _context.SanPham.Remove(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPham.Any(e => e.ID == id);
        }
    }
}
