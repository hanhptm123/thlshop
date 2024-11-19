using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SHOPTHL.Data;
using SHOPTHL.Models;

namespace SHOPTHL.Controllers
{
    public class KhachhangsController : Controller
    {
        private readonly Thlshop2Context _context;

        public KhachhangsController(Thlshop2Context context)
        {
            _context = context;
        }

        // GET: Khachhangs
        public async Task<IActionResult> Index()
        {
            var thlshop2Context = _context.Khachhangs.Include(k => k.MataikhoanNavigation);
            return View(await thlshop2Context.ToListAsync());
        }
		public async Task<IActionResult> Thongtincanhan()
		{
			// Lấy Makh từ TempData
			int id = Convert.ToInt32(TempData["Makh"]);

			var thlshop2Context = _context.Khachhangs.Include(k => k.MataikhoanNavigation).Where(k => k.Makh == id);
			return View(await thlshop2Context.ToListAsync());
		}

		// GET: Khachhangs/ViewProfile
		public async Task<IActionResult> ViewProfile()
		{
			// Lấy id của khách hàng từ session
			int? id = HttpContext.Session.GetInt32("NewCustomerId");

			if (id == null)
			{
				return NotFound();
			}

			var khachhang = await _context.Khachhangs
				.Include(k => k.MataikhoanNavigation)
				.FirstOrDefaultAsync(m => m.Makh == id);

			if (khachhang == null)
			{
				return NotFound();
			}

			return View(khachhang);
		}

		// GET: Khachhangs/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs
                .Include(k => k.MataikhoanNavigation)
                .FirstOrDefaultAsync(m => m.Makh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // GET: Khachhangs/Create
        public IActionResult Create(int Mataikhoan)
        {
            ViewData["Mataikhoan"] = Mataikhoan;
            return View();
        }


        // POST: Khachhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Khachhangs/Create
        // POST: Khachhangs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Makh,Tenkh,Sdt,Diachi,Email,Mataikhoan")] Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachhang);
                await _context.SaveChangesAsync();

                // Lưu id của khách hàng mới vào session
                HttpContext.Session.SetInt32("NewCustomerId", khachhang.Makh);

                // Tạo một địa chỉ mới và lưu vào cơ sở dữ liệu
                Diachi diachi = new Diachi
                {
                    Makh = khachhang.Makh,
                    Tennguoinhan = khachhang.Tenkh,
                    Sdt = khachhang.Sdt,
                    Diachi1 = khachhang.Diachi,
                };
                _context.Diachis.Add(diachi);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            ViewData["Mataikhoan"] = new SelectList(_context.Taikhoans, "Mataikhoan", "Mataikhoan", khachhang.Mataikhoan);
            return View(khachhang);
        }





        // GET: Khachhangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            ViewData["Mataikhoan"] = new SelectList(_context.Taikhoans, "Mataikhoan", "Mataikhoan", khachhang.Mataikhoan);
            return View(khachhang);
        }

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Makh,Tenkh,Sdt,Diachi,Email")] Khachhang khachhang)
		{
			if (id != khachhang.Makh)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					// Kiểm tra xem bản ghi khách hàng có tồn tại không
					var existingKhachHang = await _context.Khachhangs.FirstOrDefaultAsync(k => k.Makh == id);
					if (existingKhachHang != null)
					{
						// Chỉ cập nhật các trường mà chúng ta muốn cho phép chỉnh sửa
						existingKhachHang.Tenkh = khachhang.Tenkh;
						existingKhachHang.Sdt = khachhang.Sdt;
						existingKhachHang.Diachi = khachhang.Diachi;
						existingKhachHang.Email = khachhang.Email;

						_context.Update(existingKhachHang);
						await _context.SaveChangesAsync();
					}
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!KhachhangExists(khachhang.Makh))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction("ViewProfile", "Khachhangs");
			}
			return View(khachhang);
		}

		// GET: Khachhangs/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs
                .Include(k => k.MataikhoanNavigation)
                .FirstOrDefaultAsync(m => m.Makh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // POST: Khachhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang != null)
            {
                _context.Khachhangs.Remove(khachhang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachhangExists(int id)
        {
            return _context.Khachhangs.Any(e => e.Makh == id);
        }
    }
}
