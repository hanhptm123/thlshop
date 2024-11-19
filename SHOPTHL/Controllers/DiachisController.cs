using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SHOPTHL.Data;

namespace SHOPTHL.Controllers
{
    public class DiachisController : Controller
    {
        private readonly Thlshop2Context _context;

        public DiachisController(Thlshop2Context context)
        {
            _context = context;
        }
        // GET: Diachis/ListByCustomer
        public async Task<IActionResult> Diachitheomakh()
        {
            // Lấy mã khách hàng từ session
            int? customerId = HttpContext.Session.GetInt32("NewCustomerId");

            // Kiểm tra xem session có tồn tại và mã khách hàng có hợp lệ hay không
            if (customerId == null)
            {
                // Xử lý khi session không tồn tại hoặc không hợp lệ
                return RedirectToAction("Index", "Home"); // hoặc bạn có thể xử lý khác tùy thuộc vào yêu cầu của ứng dụng
            }

            // Lấy danh sách địa chỉ của khách hàng từ cơ sở dữ liệu
            var diachis = await _context.Diachis
                .Where(d => d.Makh == customerId)
                .ToListAsync();

            return View(diachis);
        }

        // GET: Diachis

        public async Task<IActionResult> Index()
        {
            var thlshop2Context = _context.Diachis.Include(d => d.MakhNavigation);
            return View(await thlshop2Context.ToListAsync());
        }

        // GET: Diachis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diachi = await _context.Diachis
                .Include(d => d.MakhNavigation)
                .FirstOrDefaultAsync(m => m.Madiachi == id);
            if (diachi == null)
            {
                return NotFound();
            }

            return View(diachi);
        }

		// GET: Diachis/Create
		public IActionResult Create()
		{
			// Lấy mã khách hàng từ session
			int? customerId = HttpContext.Session.GetInt32("NewCustomerId");

			// Kiểm tra xem session có tồn tại và mã khách hàng có hợp lệ hay không
			if (customerId == null)
			{
				// Xử lý khi session không tồn tại hoặc không hợp lệ
				return RedirectToAction("Index", "Home"); // hoặc bạn có thể xử lý khác tùy thuộc vào yêu cầu của ứng dụng
			}

			// Truyền mã khách hàng vào SelectList
			ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh", customerId);
			return View();
		}


		// POST: Diachis/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Madiachi,Makh,Tennguoinhan,Sdt,Diachi1")] Diachi diachi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diachi);
                await _context.SaveChangesAsync();
                return RedirectToAction("Diachitheomakh", "Diachis");
            }
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh", diachi.Makh);
            return View(diachi);
        }

        // GET: Diachis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diachi = await _context.Diachis.FindAsync(id);
            if (diachi == null)
            {
                return NotFound();
            }
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh", diachi.Makh);
            return View(diachi);
        }

        // POST: Diachis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Madiachi,Makh,Tennguoinhan,Sdt,Diachi1")] Diachi diachi)
        {
            if (id != diachi.Madiachi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diachi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiachiExists(diachi.Madiachi))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Diachitheomakh","Diachis");
            }
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh", diachi.Makh);
            return View(diachi);
        }

        // GET: Diachis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diachi = await _context.Diachis
                .Include(d => d.MakhNavigation)
                .FirstOrDefaultAsync(m => m.Madiachi == id);
            if (diachi == null)
            {
                return NotFound();
            }

            return View(diachi);
        }

        // POST: Diachis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diachi = await _context.Diachis.FindAsync(id);
            if (diachi != null)
            {
                _context.Diachis.Remove(diachi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Diachitheomakh", "Diachis");
        }

        private bool DiachiExists(int id)
        {
            return _context.Diachis.Any(e => e.Madiachi == id);
        }
    }
}
