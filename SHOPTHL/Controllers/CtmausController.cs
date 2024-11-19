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
    public class CtmausController : Controller
    {
        private readonly Thlshop2Context _context;

        public CtmausController(Thlshop2Context context)
        {
            _context = context;
        }

        // GET: Ctmaus
        public async Task<IActionResult> Index()
        {
            var thlshop2Context = _context.Ctmaus.Include(c => c.MamauNavigation).Include(c => c.MaspNavigation);
            return View(await thlshop2Context.ToListAsync());
        }

        // GET: Ctmaus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ctmau = await _context.Ctmaus
                .Include(c => c.MamauNavigation)
                .Include(c => c.MaspNavigation)
                .FirstOrDefaultAsync(m => m.Mactmau == id);
            if (ctmau == null)
            {
                return NotFound();
            }

            return View(ctmau);
        }

        // GET: Ctmaus/Create
        public IActionResult Create()
        {
            ViewData["Mamau"] = new SelectList(_context.Maus, "Mamau", "Mamau");
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Masp");
            return View();
        }

        // POST: Ctmaus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mactmau,Mamau,Masp,Hinhanh")] Ctmau ctmau)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ctmau);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Mamau"] = new SelectList(_context.Maus, "Mamau", "Mamau", ctmau.Mamau);
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Masp", ctmau.Masp);
            return View(ctmau);
        }

        // GET: Ctmaus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ctmau = await _context.Ctmaus.FindAsync(id);
            if (ctmau == null)
            {
                return NotFound();
            }
            ViewData["Mamau"] = new SelectList(_context.Maus, "Mamau", "Mamau", ctmau.Mamau);
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Masp", ctmau.Masp);
            return View(ctmau);
        }

        // POST: Ctmaus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mactmau,Mamau,Masp,Hinhanh")] Ctmau ctmau)
        {
            if (id != ctmau.Mactmau)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ctmau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CtmauExists(ctmau.Mactmau))
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
            ViewData["Mamau"] = new SelectList(_context.Maus, "Mamau", "Mamau", ctmau.Mamau);
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Masp", ctmau.Masp);
            return View(ctmau);
        }

        // GET: Ctmaus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ctmau = await _context.Ctmaus
                .Include(c => c.MamauNavigation)
                .Include(c => c.MaspNavigation)
                .FirstOrDefaultAsync(m => m.Mactmau == id);
            if (ctmau == null)
            {
                return NotFound();
            }

            return View(ctmau);
        }

        // POST: Ctmaus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ctmau = await _context.Ctmaus.FindAsync(id);
            if (ctmau != null)
            {
                _context.Ctmaus.Remove(ctmau);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CtmauExists(string id)
        {
            return _context.Ctmaus.Any(e => e.Mactmau == id);
        }
    }
}
