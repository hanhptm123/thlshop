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
    public class TrangthaisController : Controller
    {
        private readonly Thlshop2Context _context;

        public TrangthaisController(Thlshop2Context context)
        {
            _context = context;
        }

        // GET: Trangthais
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trangthais.ToListAsync());
        }

        // GET: Trangthais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangthai = await _context.Trangthais
                .FirstOrDefaultAsync(m => m.Matrangthai == id);
            if (trangthai == null)
            {
                return NotFound();
            }

            return View(trangthai);
        }

        // GET: Trangthais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trangthais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matrangthai,Tentrangthai")] Trangthai trangthai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trangthai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trangthai);
        }

        // GET: Trangthais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangthai = await _context.Trangthais.FindAsync(id);
            if (trangthai == null)
            {
                return NotFound();
            }
            return View(trangthai);
        }

        // POST: Trangthais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Matrangthai,Tentrangthai")] Trangthai trangthai)
        {
            if (id != trangthai.Matrangthai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trangthai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrangthaiExists(trangthai.Matrangthai))
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
            return View(trangthai);
        }

        // GET: Trangthais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangthai = await _context.Trangthais
                .FirstOrDefaultAsync(m => m.Matrangthai == id);
            if (trangthai == null)
            {
                return NotFound();
            }

            return View(trangthai);
        }

        // POST: Trangthais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trangthai = await _context.Trangthais.FindAsync(id);
            if (trangthai != null)
            {
                _context.Trangthais.Remove(trangthai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrangthaiExists(int id)
        {
            return _context.Trangthais.Any(e => e.Matrangthai == id);
        }
    }
}
