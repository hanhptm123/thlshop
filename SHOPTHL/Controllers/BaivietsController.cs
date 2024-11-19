using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SHOPTHL.Data;

namespace SHOPTHL.Controllers
{
    public class BaivietsController : Controller
    {
        private readonly Thlshop2Context _context;

        public BaivietsController(Thlshop2Context context)
        {
            _context = context;
        }

        // GET: Baiviets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Baiviets.ToListAsync());
        }

		public async Task<IActionResult> Blog()
		{
			return View(await _context.Baiviets.ToListAsync());
		}
		// GET: Baiviets/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiviet = await _context.Baiviets
                .FirstOrDefaultAsync(m => m.Mabaiviet == id);
            if (baiviet == null)
            {
                return NotFound();
            }

            return View(baiviet);
        }

		// GET: Baiviets/Create
		[Authorize(Roles = "Administrator")]
		public IActionResult Create()
        {
            return View();
        }

        // POST: Baiviets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mabaiviet,Tenbaiviet,Noidung,Hinhanh,Ngaydang")] Baiviet baiviet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baiviet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baiviet);
        }

		// GET: Baiviets/Edit/5
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiviet = await _context.Baiviets.FindAsync(id);
            if (baiviet == null)
            {
                return NotFound();
            }
            return View(baiviet);
        }

        // POST: Baiviets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Edit(int id, [Bind("Mabaiviet,Tenbaiviet,Noidung,Hinhanh,Ngaydang")] Baiviet baiviet)
        {
            if (id != baiviet.Mabaiviet)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baiviet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaivietExists(baiviet.Mabaiviet))
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
            return View(baiviet);
        }

		// GET: Baiviets/Delete/5
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiviet = await _context.Baiviets
                .FirstOrDefaultAsync(m => m.Mabaiviet == id);
            if (baiviet == null)
            {
                return NotFound();
            }

            return View(baiviet);
        }

        // POST: Baiviets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baiviet = await _context.Baiviets.FindAsync(id);
            if (baiviet != null)
            {
                _context.Baiviets.Remove(baiviet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaivietExists(int id)
        {
            return _context.Baiviets.Any(e => e.Mabaiviet == id);
        }
    }
}
