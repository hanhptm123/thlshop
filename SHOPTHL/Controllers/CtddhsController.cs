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
    public class CtddhsController : Controller
    {
        private readonly Thlshop2Context _context;

        public CtddhsController(Thlshop2Context context)
        {
            _context = context;
        }

        // GET: Ctddhs
        public async Task<IActionResult> Index()
        {
            var thlshop2Context = _context.Ctddhs.Include(c => c.MaddhNavigation).Include(c => c.MaspNavigation);
            return View(await thlshop2Context.ToListAsync());
        }
        public async Task<IActionResult> Ctddhtheomakh(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Truy vấn chi tiết đơn đặt hàng dựa trên mã đơn đặt hàng
            var chiTietDonDatHangs = await _context.Ctddhs
                .Where(ctddh => ctddh.MaddhNavigation.Maddh == id)
                .Include(ctddh => ctddh.MaddhNavigation) // Bổ sung dữ liệu liên quan nếu cần thiết
                .Include(ctddh => ctddh.MaspNavigation)
                .ToListAsync();

            if (chiTietDonDatHangs == null || !chiTietDonDatHangs.Any())
            {
                return NotFound();
            }

            ViewBag.MaDonDatHang = id; // Lưu mã đơn đặt hàng vào ViewBag để truy cập từ view

            return View(chiTietDonDatHangs);
        }
        public async Task<IActionResult> Ctddhdagiao(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Truy vấn chi tiết đơn đặt hàng dựa trên mã đơn đặt hàng
            var chiTietDonDatHangs = await _context.Ctddhs
                .Where(ctddh => ctddh.MaddhNavigation.Maddh == id)
                .Include(ctddh => ctddh.MaddhNavigation) // Bổ sung dữ liệu liên quan nếu cần thiết
                .Include(ctddh => ctddh.MaspNavigation)
                .ToListAsync();

            if (chiTietDonDatHangs == null || !chiTietDonDatHangs.Any())
            {
                return NotFound();
            }

            ViewBag.MaDonDatHang = id; // Lưu mã đơn đặt hàng vào ViewBag để truy cập từ view

            return View(chiTietDonDatHangs);
        }
        public async Task<IActionResult> Ctddhadmin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Truy vấn chi tiết đơn đặt hàng dựa trên mã đơn đặt hàng
            var chiTietDonDatHangs = await _context.Ctddhs
                .Where(ctddh => ctddh.MaddhNavigation.Maddh == id)
                .Include(ctddh => ctddh.MaddhNavigation) // Bổ sung dữ liệu liên quan nếu cần thiết
                .Include(ctddh => ctddh.MaspNavigation)
                .ToListAsync();

            if (chiTietDonDatHangs == null || !chiTietDonDatHangs.Any())
            {
                return NotFound();
            }

            ViewBag.MaDonDatHang = id; // Lưu mã đơn đặt hàng vào ViewBag để truy cập từ view

            return View(chiTietDonDatHangs);
        }



        // GET: Ctddhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ctddh = await _context.Ctddhs
                .Include(c => c.MaddhNavigation)
                .Include(c => c.MaspNavigation)
                .FirstOrDefaultAsync(m => m.Mactddh == id);
            if (ctddh == null)
            {
                return NotFound();
            }

            return View(ctddh);
        }

        // GET: Ctddhs/Create
        public IActionResult Create()
        {
            ViewData["Maddh"] = new SelectList(_context.Dondathangs, "Maddh", "Maddh");
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Tensp");
            return View();
        }

        // POST: Ctddhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mactddh,Maddh,Masp,Soluong,Thanhtien")] Ctddh ctddh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ctddh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Maddh"] = new SelectList(_context.Dondathangs, "Maddh", "Maddh", ctddh.Maddh);
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Tensp", ctddh.Masp);
            return View(ctddh);
        }

        // GET: Ctddhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ctddh = await _context.Ctddhs.FindAsync(id);
            if (ctddh == null)
            {
                return NotFound();
            }
            ViewData["Maddh"] = new SelectList(_context.Dondathangs, "Maddh", "Maddh", ctddh.Maddh);
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Tensp", ctddh.Masp);
            return View(ctddh);
        }

        // POST: Ctddhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mactddh,Maddh,Masp,Soluong,Thanhtien")] Ctddh ctddh)
        {
            if (id != ctddh.Mactddh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ctddh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CtddhExists(ctddh.Mactddh))
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
            ViewData["Maddh"] = new SelectList(_context.Dondathangs, "Maddh", "Maddh", ctddh.Maddh);
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Tensp", ctddh.Masp);
            return View(ctddh);
        }

        // GET: Ctddhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ctddh = await _context.Ctddhs
                .Include(c => c.MaddhNavigation)
                .Include(c => c.MaspNavigation)
                .FirstOrDefaultAsync(m => m.Mactddh == id);
            if (ctddh == null)
            {
                return NotFound();
            }

            return View(ctddh);
        }

        // POST: Ctddhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ctddh = await _context.Ctddhs.FindAsync(id);
            if (ctddh != null)
            {
                _context.Ctddhs.Remove(ctddh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CtddhExists(int id)
        {
            return _context.Ctddhs.Any(e => e.Mactddh == id);
        }
    }
}
