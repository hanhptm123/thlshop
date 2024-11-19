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
    public class PtvanchuyensController : Controller
    {
        private readonly Thlshop2Context _context;

        public PtvanchuyensController(Thlshop2Context context)
        {
            _context = context;
        }

        // GET: Ptvanchuyens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ptvanchuyens.ToListAsync());
        }

        // GET: Ptvanchuyens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptvanchuyen = await _context.Ptvanchuyens
                .FirstOrDefaultAsync(m => m.Maptvc == id);
            if (ptvanchuyen == null)
            {
                return NotFound();
            }

            return View(ptvanchuyen);
        }

        // GET: Ptvanchuyens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ptvanchuyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Maptvc,Tenptvc,Phivc")] Ptvanchuyen ptvanchuyen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptvanchuyen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ptvanchuyen);
        }

        // GET: Ptvanchuyens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptvanchuyen = await _context.Ptvanchuyens.FindAsync(id);
            if (ptvanchuyen == null)
            {
                return NotFound();
            }
            return View(ptvanchuyen);
        }

        // POST: Ptvanchuyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Maptvc,Tenptvc,Phivc")] Ptvanchuyen ptvanchuyen)
        {
            if (id != ptvanchuyen.Maptvc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptvanchuyen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtvanchuyenExists(ptvanchuyen.Maptvc))
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
            return View(ptvanchuyen);
        }

        // GET: Ptvanchuyens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptvanchuyen = await _context.Ptvanchuyens
                .FirstOrDefaultAsync(m => m.Maptvc == id);
            if (ptvanchuyen == null)
            {
                return NotFound();
            }

            return View(ptvanchuyen);
        }

        // POST: Ptvanchuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptvanchuyen = await _context.Ptvanchuyens.FindAsync(id);
            if (ptvanchuyen != null)
            {
                _context.Ptvanchuyens.Remove(ptvanchuyen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtvanchuyenExists(int id)
        {
            return _context.Ptvanchuyens.Any(e => e.Maptvc == id);
        }
    }
}
