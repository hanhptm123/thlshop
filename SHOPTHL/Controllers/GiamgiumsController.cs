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
    public class GiamgiumsController : Controller
    {
        private readonly Thlshop2Context _context;

        public GiamgiumsController(Thlshop2Context context)
        {
            _context = context;
        }

        // GET: Giamgiums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Giamgia.ToListAsync());
        }

        // GET: Giamgiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giamgium = await _context.Giamgia
                .FirstOrDefaultAsync(m => m.Magiamgia == id);
            if (giamgium == null)
            {
                return NotFound();
            }

            return View(giamgium);
        }

        // GET: Giamgiums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Giamgiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Magiamgia,Ngaybatdau,Ngayketthuc,Phantramgiam")] Giamgium giamgium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giamgium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(giamgium);
        }

        // GET: Giamgiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giamgium = await _context.Giamgia.FindAsync(id);
            if (giamgium == null)
            {
                return NotFound();
            }
            return View(giamgium);
        }

        // POST: Giamgiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Magiamgia,Ngaybatdau,Ngayketthuc,Phantramgiam")] Giamgium giamgium)
        {
            if (id != giamgium.Magiamgia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giamgium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiamgiumExists(giamgium.Magiamgia))
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
            return View(giamgium);
        }

        // GET: Giamgiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giamgium = await _context.Giamgia
                .FirstOrDefaultAsync(m => m.Magiamgia == id);
            if (giamgium == null)
            {
                return NotFound();
            }

            return View(giamgium);
        }

        // POST: Giamgiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giamgium = await _context.Giamgia.FindAsync(id);
            if (giamgium != null)
            {
                _context.Giamgia.Remove(giamgium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiamgiumExists(int id)
        {
            return _context.Giamgia.Any(e => e.Magiamgia == id);
        }
    }
}
