using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SHOPTHL.Data;
using X.PagedList;
using X.PagedList.Mvc.Core;
using X.PagedList.Mvc.Bootstrap4;

namespace SHOPTHL.Controllers
{
    public class DondathangsController : Controller
    {
        private readonly Thlshop2Context _context;

        public DondathangsController(Thlshop2Context context)
        {
            _context = context;
        }

        // GET: Dondathangs
        public async Task<IActionResult> Index(int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pagesize = 5;
            var thlshop2Context = _context.Dondathangs.Include(d => d.MakhNavigation).Include(d => d.MaptvcNavigation).Include(d => d.MatrangthaiNavigation).ToPagedList(page, pagesize);
            return View(thlshop2Context);

        }

        public async Task<IActionResult> Dondathangtheoloaiadmin(int? id)
        {
            var thlshop2Context = _context.Dondathangs.Include(d => d.MakhNavigation).Include(d => d.MaptvcNavigation).Include(d => d.MatrangthaiNavigation).Where(s => s.Matrangthai == id);
            return View(await thlshop2Context.ToListAsync());
        }
        //public async Task<IActionResult> Dondathangtheomakh()
        //{
        //    // Lấy id của khách hàng từ session
        //    int? id = HttpContext.Session.GetInt32("NewCustomerId");

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var dondathangs = _context.Dondathangs
        //        .Include(d => d.MakhNavigation)
        //        .Include(d => d.MaptvcNavigation)
        //        .Include(d => d.MatrangthaiNavigation)
        //        .Where(d => d.Makh == id);

        //    if (dondathangs == null || !dondathangs.Any())
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.MaKhachHang = id; // Lưu mã khách hàng vào ViewBag để truy cập từ view

        //    return View(await dondathangs.ToListAsync());
        //}


        public async Task<IActionResult> Dondathangtheomakh()
        {
            // Lấy id của khách hàng từ session
            int? id = HttpContext.Session.GetInt32("NewCustomerId");

            if (id == null)
            {
                return NotFound();
            }

            var dondathangs = await _context.Dondathangs
                .Include(d => d.MakhNavigation)
                .Include(d => d.MaptvcNavigation)
                .Include(d => d.MatrangthaiNavigation)
                .Where(d => d.Makh == id)
                .ToListAsync();

            if (dondathangs == null || !dondathangs.Any())
            {
                return NotFound();
            }

            ViewBag.MaKhachHang = id; // Lưu mã khách hàng vào ViewBag để truy cập từ view

            return View(dondathangs);
        }

        
        
        [HttpPost]
        public async Task<IActionResult> HuyDDH(int id, string lydo, string lydoKhacInput)
        {
            var donDatHang = await _context.Dondathangs.FindAsync(id);

            if (donDatHang == null)
            {
                return NotFound();
            }

            string lydoHuy = lydo;
            if (lydo == "Khác")
            {
                // Sử dụng lý do được nhập nếu lựa chọn là "Khác"
                lydoHuy = lydoKhacInput;
            }

            // Kiểm tra nếu đơn hàng đã được giao thì không thể hủy
            if (donDatHang.Matrangthai ==4)
            {
                TempData["ErrorMessage"] = "Đơn hàng đã được giao, không thể hủy.";
                return RedirectToAction(nameof(Dondathangtheomakh));
            }

            // Kiểm tra nếu đơn hàng đã bị hủy thì không thể hủy lại
            if (donDatHang.Matrangthai == 5)
            {
                TempData["ErrorMessage"] = "Đơn hàng đã được hủy trước đó.";
                return RedirectToAction(nameof(Dondathangtheomakh));
            }

            // Lưu lý do hủy vào trường Lydohuy của đơn hàng
            donDatHang.Lydohuy = lydoHuy;

            // Cập nhật trạng thái của đơn hàng thành "Đã hủy"
            donDatHang.Matrangthai = 5; // Gán mã trạng thái "Đã hủy"
            _context.Update(donDatHang);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đã hủy đơn hàng thành công.";
            return RedirectToAction(nameof(Dondathangtheomakh));
        }



        // GET: Dondathangs/TongDoanhThuTheoThang
        /*   public IActionResult TongDoanhThuTheoThang(int year, int month)
           {
               // Kiểm tra nếu năm và tháng được cung cấp hợp lệ
               if (year <= 0 || month <= 0 || month > 12)
               {
                   return BadRequest("Năm hoặc tháng không hợp lệ.");
               }

               // Tính ngày đầu tiên và cuối cùng của tháng được chỉ định
               DateTime startDate = new DateTime(year, month, 1);
               DateTime endDate = startDate.AddMonths(1).AddDays(-1);

               // Truy vấn các đơn đặt hàng trong khoảng thời gian đó
               var dondathangs = _context.Dondathangs
                   .Where(d => d.Ngaydh >= startDate && d.Ngaydh <= endDate)
                   .ToList();

               // Tính tổng doanh thu từ các đơn đặt hàng
               decimal tongDoanhThu = (decimal)dondathangs.Sum(d => d.Tongtien);

               // Trả về view kèm theo dữ liệu tổng doanh thu
               ViewBag.Thang = $"{month}/{year}";
               return View("TongDoanhThuTheoThang", tongDoanhThu);
           }*/

        public IActionResult TongDoanhThuTheoThang(int year, int month)
        {
            // Kiểm tra nếu năm và tháng được cung cấp hợp lệ
            if (year <= 0 || month <= 0 || month > 12)
            {
                return BadRequest("Năm hoặc tháng không hợp lệ.");
            }

            // Tính ngày đầu tiên và cuối cùng của tháng được chỉ định
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            // Truy vấn các đơn đặt hàng đã giao trong khoảng thời gian đó
            var dondathangs = _context.Dondathangs
                .Include(d => d.MatrangthaiNavigation)
                .Where(d => d.Ngaydh >= startDate && d.Ngaydh <= endDate && d.MatrangthaiNavigation.Tentrangthai == "Đã giao")
                .ToList();

            // Tính tổng doanh thu từ các đơn đặt hàng đã giao
            decimal tongDoanhThu = dondathangs.Sum(d => d.Tongtien ?? 0);

            // Trả về view kèm theo dữ liệu tổng doanh thu và danh sách đơn hàng đã giao
            ViewBag.Thang = $"{month}/{year}";
            return View("TongDoanhThuTheoThang", new TongDoanhThuViewModel
            {
                TongDoanhThu = tongDoanhThu,
                DanhSachDonDatHang = dondathangs
            });
        }

        // GET: Dondathangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dondathang = await _context.Dondathangs
                .Include(d => d.MakhNavigation)
                .Include(d => d.MaptvcNavigation)
                .Include(d => d.MatrangthaiNavigation)
                .FirstOrDefaultAsync(m => m.Maddh == id);
            if (dondathang == null)
            {
                return NotFound();
            }

            return View(dondathang);
        }

        // GET: Dondathangs/Create
        public IActionResult Create()
        {
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh");
            ViewData["Maptvc"] = new SelectList(_context.Ptvanchuyens, "Maptvc", "Tenptvc");
            ViewData["Matrangthai"] = new SelectList(_context.Trangthais, "Matrangthai", "Tentrangthai");
            return View();
        }

        // POST: Dondathangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Maddh,Tongtien,Makh,Maptvc,Ngaydh,Matrangthai,Hoten,Diachi,Sdt")] Dondathang dondathang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dondathang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh", dondathang.Makh);
            ViewData["Maptvc"] = new SelectList(_context.Ptvanchuyens, "Maptvc", "Tenptvc", dondathang.Maptvc);
            ViewData["Matrangthai"] = new SelectList(_context.Trangthais, "Matrangthai", "Tentrangthai", dondathang.Matrangthai);
            return View(dondathang);
        }

        // GET: Dondathangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dondathang = await _context.Dondathangs.FindAsync(id);
            if (dondathang == null)
            {
                return NotFound();
            }
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh", dondathang.Makh);
            ViewData["Maptvc"] = new SelectList(_context.Ptvanchuyens, "Maptvc", "Tenptvc", dondathang.Maptvc);
            ViewData["Matrangthai"] = new SelectList(_context.Trangthais, "Matrangthai", "Tentrangthai", dondathang.Matrangthai);
            return View(dondathang);
        }

        // POST: Dondathangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Maddh,Tongtien,Makh,Maptvc,Ngaydh,Matrangthai,Hoten,Diachi,Sdt")] Dondathang dondathang)
        {
            if (id != dondathang.Maddh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dondathang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DondathangExists(dondathang.Maddh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh", dondathang.Makh);
            ViewData["Maptvc"] = new SelectList(_context.Ptvanchuyens, "Maptvc", "Tenptvc", dondathang.Maptvc);
            ViewData["Matrangthai"] = new SelectList(_context.Trangthais, "Matrangthai", "Tentrangthai", dondathang.Matrangthai);
            return View(dondathang);
        }

        // GET: Dondathangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dondathang = await _context.Dondathangs
                .Include(d => d.MakhNavigation)
                .Include(d => d.MaptvcNavigation)
                .Include(d => d.MatrangthaiNavigation)
                .FirstOrDefaultAsync(m => m.Maddh == id);
            if (dondathang == null)
            {
                return NotFound();
            }

            return View(dondathang);
        }

        // POST: Dondathangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dondathang = await _context.Dondathangs.FindAsync(id);
            if (dondathang != null)
            {
                _context.Dondathangs.Remove(dondathang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DondathangExists(int id)
        {
            return _context.Dondathangs.Any(e => e.Maddh == id);
        }
    }
}
