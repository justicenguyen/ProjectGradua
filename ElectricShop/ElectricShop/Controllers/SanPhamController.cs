using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectricShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace ElectricShop.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ElectricShopContext _context;
        private IHostingEnvironment hostingEnv;

        public SanPhamController(ElectricShopContext context, IHostingEnvironment env)
        {
            _context = context;
            this.hostingEnv = env;
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


        public async Task<IActionResult> ChiTietSanPham(string id)
        {
            var sanpham = await _context.SanPham
                .SingleOrDefaultAsync(m => m.TenSPKhongDau == id);
            var dsloaisanpham = from lsp in _context.LoaiSanPham
                                select lsp;
            ViewData["menu"] = dsloaisanpham;
            var nhasanxuat = await _context.NhaSanXuat
               .SingleOrDefaultAsync(m => m.ID == sanpham.NhaSanXuat);
            string HangSanXuat = nhasanxuat.TenNSXCoDau;
            var dssanphamlienquan = (from splq in _context.SanPham
                                     where splq.LoaiSanPham == sanpham.LoaiSanPham && splq.TenSPKhongDau != id
                                     select splq).Take(4);
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

        public async Task<ActionResult> BinhLuan1(BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                var ngayht = DateTime.Now.ToLocalTime();
                binhLuan.ThoiGian = ngayht;
                _context.Add(binhLuan);
                await _context.SaveChangesAsync();


                string rs="<div class='review-item clearfix'>"+
                                "<div class='review-item-submitted'>"+
                                    "<strong>"+binhLuan.HoTen+"</strong>"+
                                    "<em>"+ngayht+"</em>"+
                                "</div>"+
                                "<div class='review-item-content'>"+
                                    "<p> "+binhLuan.NoiDung+"</p>"+
                                "</div>"+
                            "</div>";


                return Content(rs, "text/plain");
            }
            return View("ChiTiet", binhLuan);
        }



        public async Task<IActionResult> DanhSachSanPhamTheoLoai1(string id, string hang, string gia)
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
                if (gia.Equals("duoi-5-trieu"))
                {
                    dssanpham = dssanpham.Where(g => g.GiaBan < 5000000);
                    ViewData["gia"] = "duoi-5-trieu";
                }
                else
                {
                    if (gia.Equals("tu-5-7-trieu"))
                    {
                        dssanpham = dssanpham.Where(g => (g.GiaBan >= 5000000 && g.GiaBan < 7000000));
                        ViewData["gia"] = "tu-5-7-trieu";
                    }
                    else
                    {
                        if (gia.Equals("tu-7-10-trieu"))
                        {
                            dssanpham = dssanpham.Where(g => (g.GiaBan >= 7000000 && g.GiaBan < 10000000));
                            ViewData["gia"] = "tu-7-10-trieu";
                        }
                        else
                        {
                            dssanpham = dssanpham.Where(g => g.GiaBan > 10000000);
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


        public async Task<IActionResult> DanhSachSanPhamKhuyenMai1(string loai,string hang, string gia)
        {
            var dsSanPhamKhuyenMai = from sp in _context.SanPham
                                     where sp.GiaGiam > 0
                                     select sp;

            var dsIDLoaiSanPhamKhuyenMai = from lsp in dsSanPhamKhuyenMai
                                         group lsp by lsp.LoaiSanPham;
            var dsLoaiSanPhamKhuyenMai = from lsp in _context.LoaiSanPham
                                         join k in dsIDLoaiSanPhamKhuyenMai on lsp.ID equals k.Key
                                         select lsp;
            //Lấy các nhà sản xuất tương ứng của loại sản phẩm này hiện co ở cửa hàng
            var dsnsx = from key in dsSanPhamKhuyenMai
                        group key by key.NhaSanXuat;
            var dsnhasanxuat = from nsx in _context.NhaSanXuat
                               join k in dsnsx on nsx.ID equals k.Key
                               select nsx;
            //Lọc theo loại sản phẩm
            ViewData["loai"] = "";
            if (!String.IsNullOrEmpty(loai))
            {
                var loaisanpham = await _context.LoaiSanPham
                    .SingleOrDefaultAsync(m => m.TenLoaiSPKhongDau == loai);
                dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(h => h.LoaiSanPham.Equals(loaisanpham.ID));
                ViewData["loai"] = loai;
            }
            // Lọc sản phẩm theo hãng sãn xuất
            ViewData["hang"] = "";
            if (!String.IsNullOrEmpty(hang))
            {
                var nhasanxuat = await _context.NhaSanXuat
                    .SingleOrDefaultAsync(m => m.TenNSXKhongDau == hang);
                dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(h => h.NhaSanXuat.Equals(nhasanxuat.ID));
                ViewData["hang"] = hang;
            }
            //Lọc sản phẩm theo giá
            ViewData["gia"] = "";
            if (!String.IsNullOrEmpty(gia))
            {
                if (gia.Equals("duoi-5-trieu"))
                {
                    dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(g => g.GiaBan < 5000000);
                    ViewData["gia"] = "duoi-5-trieu";
                }
                else
                {
                    if (gia.Equals("tu-5-7-trieu"))
                    {
                        dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(g => (g.GiaBan >= 5000000 && g.GiaBan < 7000000));
                        ViewData["gia"] = "tu-5-7-trieu";
                    }
                    else
                    {
                        if (gia.Equals("tu-7-10-trieu"))
                        {
                            dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(g => (g.GiaBan >= 7000000 && g.GiaBan < 10000000));
                            ViewData["gia"] = "tu-7-10-trieu";
                        }
                        else
                        {
                            dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(g => g.GiaBan > 10000000);
                            ViewData["gia"] = "tren-10-trieu";
                        }
                    }
                }
            }

            //Dữ liệu truyền sang View
            var dssanphamkhuyenmai = new DuLieuDanhSachSanPhamKhuyenMai();
            dssanphamkhuyenmai.dssanphamkhuyenmai = await dsSanPhamKhuyenMai.ToListAsync();
            dssanphamkhuyenmai.dsnhasanxuat = await dsnhasanxuat.ToListAsync();
            dssanphamkhuyenmai.dsloaisanpham = await dsLoaiSanPhamKhuyenMai.ToListAsync();
            return View(dssanphamkhuyenmai);
        }



        public async Task<IActionResult> DanhSachSanPhamTimKiem1(string tukhoa,string loai, string hang, string gia)
        {
            var dsSanPhamKhuyenMai = from sp in _context.SanPham
                                     where sp.TenSPCoDau.Contains(tukhoa)
                                     select sp;

            var dsIDLoaiSanPhamKhuyenMai = from lsp in dsSanPhamKhuyenMai
                                           group lsp by lsp.LoaiSanPham;
            var dsLoaiSanPhamKhuyenMai = from lsp in _context.LoaiSanPham
                                         join k in dsIDLoaiSanPhamKhuyenMai on lsp.ID equals k.Key
                                         select lsp;
            //Lấy các nhà sản xuất tương ứng của loại sản phẩm này hiện co ở cửa hàng
            var dsnsx = from key in dsSanPhamKhuyenMai
                        group key by key.NhaSanXuat;
            var dsnhasanxuat = from nsx in _context.NhaSanXuat
                               join k in dsnsx on nsx.ID equals k.Key
                               select nsx;
            //Lọc theo loại sản phẩm
            ViewData["loai"] = "";
            if (!String.IsNullOrEmpty(loai))
            {
                var loaisanpham = await _context.LoaiSanPham
                    .SingleOrDefaultAsync(m => m.TenLoaiSPKhongDau == loai);
                dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(h => h.LoaiSanPham.Equals(loaisanpham.ID));
                ViewData["loai"] = loai;
            }
            // Lọc sản phẩm theo hãng sãn xuất
            ViewData["hang"] = "";
            if (!String.IsNullOrEmpty(hang))
            {
                var nhasanxuat = await _context.NhaSanXuat
                    .SingleOrDefaultAsync(m => m.TenNSXKhongDau == hang);
                dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(h => h.NhaSanXuat.Equals(nhasanxuat.ID));
                ViewData["hang"] = hang;
            }
            //Lọc sản phẩm theo giá
            ViewData["gia"] = "";
            if (!String.IsNullOrEmpty(gia))
            {
                if (gia.Equals("duoi-5-trieu"))
                {
                    dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(g => g.GiaBan < 5000000);
                    ViewData["gia"] = "duoi-5-trieu";
                }
                else
                {
                    if (gia.Equals("tu-5-7-trieu"))
                    {
                        dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(g => (g.GiaBan >= 5000000 && g.GiaBan < 7000000));
                        ViewData["gia"] = "tu-5-7-trieu";
                    }
                    else
                    {
                        if (gia.Equals("tu-7-10-trieu"))
                        {
                            dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(g => (g.GiaBan >= 7000000 && g.GiaBan < 10000000));
                            ViewData["gia"] = "tu-7-10-trieu";
                        }
                        else
                        {
                            dsSanPhamKhuyenMai = dsSanPhamKhuyenMai.Where(g => g.GiaBan > 10000000);
                            ViewData["gia"] = "tren-10-trieu";
                        }
                    }
                }
            }
            ViewData["tukhoa"] = tukhoa;
            //Dữ liệu truyền sang View
            var dssanphamkhuyenmai = new DuLieuDanhSachSanPhamKhuyenMai();
            dssanphamkhuyenmai.dssanphamkhuyenmai = await dsSanPhamKhuyenMai.ToListAsync();
            dssanphamkhuyenmai.dsnhasanxuat = await dsnhasanxuat.ToListAsync();
            dssanphamkhuyenmai.dsloaisanpham = await dsLoaiSanPhamKhuyenMai.ToListAsync();
            return View(dssanphamkhuyenmai);
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

        //Ajax load thêm sản phẩm khuyến mãi
        public IActionResult LoadThemSanPhamKhuyenMai(int idlsp)
        {
            var dsSanPhamKhuyenMaiThem = (from sp in _context.SanPham
                                         where sp.GiaGiam > 0 && sp.ID == idlsp
                                         select sp).Skip(3);
            var rs = "";

            foreach(var sp in dsSanPhamKhuyenMaiThem)
            {
                int phantram = sp.GiaGiam * 100 / sp.GiaGoc;
                rs = rs + "<div class='col-md-3'>" +
                        "<div class='product-item'>" +
                        "<div class='pi-img-wrapper'>" +
                            "<img src ='/"+@sp.HinhAnh+"' class='img-responsive' alt='Berry Lace Dress'>" +
                            "<div>" +
                                "<a href ='/" + @sp.HinhAnh + "' class='btn btn-default fancybox-button'>Zoom</a>" +
                                "<a href ='#product-pop-up' class='btn btn-default fancybox-fast-view'>View</a>" +
                           "</div>" +
                        "</div>" +
                       "<h3><a href ='shop-item.html'>"+@sp.TenSPCoDau+" </ a ></ h3 >" +
                        "<div class='pi-price'>"+@sp.GiaBan+" vnd</div>" +
                        "<div style = 'clear:both;' >" +
                            "<span style='text-decoration:line-through;'>"+@sp.GiaGoc+" vnd</span>&nbsp;<span style = 'background-color:red;' > -"+ @phantram + " %</span>" +
                        "</div>" +
                        "<div class='sticker sticker-sale'></div>" +
                       "<div style = 'margin-top:5px;' >< a asp-controller='SanPham' asp-action='ChiTietSanPham' asp-route-id='@spkm.ID' class='btn btn-default add2cart'>Add to cart</a></div>" +
                        "<div class='sticker sticker-sale'></div>" +
                    "</div>" +
                "</div>";
            }
            return Content(rs);
        }


        //Danh sách sản phẩm khuyến mãi
        public async Task<IActionResult> DanhSachSanPhamKhuyenMai()
        {

            var dsIDLoaiSanPhamKhuyenMai = from sp in _context.SanPham
                                           where sp.GiaGiam >0
                                           group sp by sp.LoaiSanPham;
            var dsLoaiSanPhamKhuyenMai= from lsp in _context.LoaiSanPham
                                        join k in dsIDLoaiSanPhamKhuyenMai on lsp.ID equals k.Key
                                        select lsp;

            return View(await dsLoaiSanPhamKhuyenMai.ToListAsync());
        }


        //Ajax thay đổi trạng thái thay đổi sản phẩm bán chạy
        public async Task<IActionResult> ThayDoiHienThi(int? idsp)
        {
            if (idsp == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.SingleOrDefaultAsync(m => m.ID == idsp);
            sanPham.HienThi = !sanPham.HienThi;
            _context.Update(sanPham);
            await _context.SaveChangesAsync();
            return Content("Thay đổi thành công");
        }

        //Ajax thay đổi trạng thái thay đổi sản phẩm bán chạy
        public async Task<IActionResult> ThayDoiSanPhamBanChay(int? idsp)
        {
            if (idsp == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.SingleOrDefaultAsync(m => m.ID == idsp);
            sanPham.SanPhamBanChay = !sanPham.SanPhamBanChay;
            _context.Update(sanPham);
            await _context.SaveChangesAsync();
            return Content("Thay đổi thành công");
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
            var dsLoaiSanPham = from lsp in _context.LoaiSanPham
                                select lsp;
            var dsNhaSanXuat = from nsx in _context.NhaSanXuat
                               select nsx;
            ViewBag.DSLoaiSanPham =  dsLoaiSanPham.ToList();
            ViewBag.DSNhaSanXuat = dsNhaSanXuat.ToList();
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenSPCoDau,TenSPKhongDau,LoaiSanPham," +
            "GiaGoc,GiaGiam,HinhAnh,MauSac,NhaSanXuat,XuatXu,BaoHanh,NgayTao,HienThi,SanPhamBanChay,SoLuong," +
            "KichThuocThung,KhoiLuongThung,KichThuocMH,DoPhanGiai,MoTa,ManHinhCong,BoXuLy,SmartTV,TanSoQuet," +
            "CongSuatLoa,CongWiFi,CongInternet,CongHDMI,CongUSB,ChiaSeThongMinh,HeDeHanh,TrinhDuyetWeb,LoaiDanMay,LoaiDauDia")] SanPham sanPham, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {
                long size = 0;
                string tenHinh = "";
                var filename = ContentDispositionHeaderValue
                                .Parse(HinhAnh.ContentDisposition)
                                .FileName
                                .Trim('"');
                tenHinh = $@"images\sanpham\" + filename;
                filename = hostingEnv.WebRootPath + $@"\images\sanpham\{filename}";
                size += HinhAnh.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    HinhAnh.CopyTo(fs);
                    fs.Flush();
                }
                sanPham.GiaBan = sanPham.GiaGoc - sanPham.GiaGiam;
                sanPham.HinhAnh = tenHinh;
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var dsLoaiSanPham = from lsp in _context.LoaiSanPham
                                select lsp;
            var dsNhaSanXuat = from nsx in _context.NhaSanXuat
                               select nsx;
            ViewBag.DSLoaiSanPham = dsLoaiSanPham.ToList();
            ViewBag.DSNhaSanXuat = dsNhaSanXuat.ToList();
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
            var dsLoaiSanPham = from lsp in _context.LoaiSanPham
                                select lsp;
            var dsNhaSanXuat = from nsx in _context.NhaSanXuat
                               select nsx;
            ViewBag.DSLoaiSanPham = dsLoaiSanPham.ToList();
            ViewBag.DSNhaSanXuat = dsNhaSanXuat.ToList();
            return View(sanPham);
        }

        // POST: SanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("ID,TenSPCoDau,TenSPKhongDau,LoaiSanPham," +
            "GiaGoc,GiaGiam,HinhAnh,MauSac,NhaSanXuat,XuatXu,BaoHanh,NgayTao,HienThi,SanPhamBanChay,SoLuong," +
            "KichThuocThung,KhoiLuongThung,KichThuocMH,DoPhanGiai,MoTa,ManHinhCong,BoXuLy,SmartTV,TanSoQuet," +
            "CongSuatLoa,CongWiFi,CongInternet,CongHDMI,CongUSB,ChiaSeThongMinh,HeDeHanh,TrinhDuyetWeb,LoaiDanMay,LoaiDauDia")] SanPham sanPham, IFormFile Hinh_Anh)
        {
            if (id != sanPham.ID)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            {
                if (sanPham.HinhAnh.Equals("changed"))
                {
                    long size = 0;
                    string tenHinh = "";
                    var filename = ContentDispositionHeaderValue
                                    .Parse(Hinh_Anh.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                    tenHinh = $@"images\sanpham\" + filename;
                    filename = hostingEnv.WebRootPath + $@"\images\sanpham\{filename}";
                    size += Hinh_Anh.Length;
                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        Hinh_Anh.CopyTo(fs);
                        fs.Flush();
                    }
                    sanPham.HinhAnh = tenHinh;
                }
                
                
                try
                {
                    sanPham.GiaBan = sanPham.GiaGoc - sanPham.GiaGiam;
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
            var dsLoaiSanPham = from lsp in _context.LoaiSanPham
                                select lsp;
            var dsNhaSanXuat = from nsx in _context.NhaSanXuat
                               select nsx;
            ViewBag.DSLoaiSanPham = dsLoaiSanPham.ToList();
            ViewBag.DSNhaSanXuat = dsNhaSanXuat.ToList();
            return View(sanPham);
        }

        // GET: SanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sanPham = await _context.SanPham.SingleOrDefaultAsync(m => m.ID == id);
            _context.SanPham.Remove(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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
