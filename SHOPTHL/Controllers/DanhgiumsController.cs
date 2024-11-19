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
    public class DanhgiumsController : Controller
    {
        private readonly Thlshop2Context _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public DanhgiumsController(Thlshop2Context context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Danhgiums
        public async Task<IActionResult> Index()
        {
            var thlshop2Context = _context.Danhgia.Include(d => d.MakhNavigation).Include(d => d.MaspNavigation);
            return View(await thlshop2Context.ToListAsync());
        }
        public async Task<IActionResult> Danhgiatheomasp(int? id)
        {
            var thlshop2Context = _context.Danhgia.Include(d => d.MakhNavigation).Include(d => d.MaspNavigation).Where(s => s.Masp == id);
            return View(await thlshop2Context.ToListAsync());
        }

        // GET: Danhgiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhgium = await _context.Danhgia
                .Include(d => d.MakhNavigation)
                .Include(d => d.MaspNavigation)
                .FirstOrDefaultAsync(m => m.Madanhgia == id);
            if (danhgium == null)
            {
                return NotFound();
            }

            return View(danhgium);
        }

        // GET: Danhgiums/Create
        public IActionResult Create(int? Masp)
        {
            // Lấy mã khách hàng từ session
            int? customerId = HttpContext.Session.GetInt32("NewCustomerId");

            // Kiểm tra xem session có tồn tại và mã khách hàng có hợp lệ hay không
            if (customerId == null)
            {
                // Xử lý khi session không tồn tại hoặc không hợp lệ
                return RedirectToAction("Index", "Home"); // hoặc bạn có thể xử lý khác tùy thuộc vào yêu cầu của ứng dụng
            }

            // Truyền mã sản phẩm và mã khách hàng vào ViewBag hoặc ViewData
            ViewBag.Masp = Masp;
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh", customerId);
            return View();
        }

        // POST: Danhgiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Madanhgia,Masp,Makh,Ngaydanhgia,Loidanhgia,Diemdanhgia,Hinhanh")] Danhgium danhgium, IFormFile Hinhanh)
        {
            // Lấy mã khách hàng từ session
            int? customerId = HttpContext.Session.GetInt32("NewCustomerId");

            // Kiểm tra xem session có tồn tại và mã khách hàng có hợp lệ hay không
            if (customerId == null)
            {
                // Xử lý khi session không tồn tại hoặc không hợp lệ
                return RedirectToAction("Index", "Home");
            }

            // Kiểm tra xem người dùng đã viết đánh giá cho sản phẩm này chưa
            var existingReview = _context.Danhgia.FirstOrDefault(d => d.Masp == danhgium.Masp && d.Makh == customerId);
            if (existingReview != null)
            {
                // Nếu đã viết đánh giá, gửi thông điệp thông báo cho view
                ViewBag.ErrorMessage = "Bạn đã đánh giá sản phẩm này rồi!";
                return View(danhgium);
            }


            // Gán mã khách hàng từ session vào đánh giá
            danhgium.Makh = customerId.Value;

            if (ModelState.IsValid)
            {
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (Hinhanh != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(Hinhanh.FileName);
                    string extension = Path.GetExtension(Hinhanh.FileName);
                    danhgium.Hinhanh = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await Hinhanh.CopyToAsync(fileStream);
                    }
                }

                _context.Add(danhgium);
                await _context.SaveChangesAsync();
                return RedirectToAction("CtSp", "Sanphams", new { id = danhgium.Masp });
            }
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh", danhgium.Makh);
            return View(danhgium);
        }


        // GET: Danhgiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhgium = await _context.Danhgia.FindAsync(id);
            if (danhgium == null)
            {
                return NotFound();
            }
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh", danhgium.Makh);
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Masp", danhgium.Masp);
            return View(danhgium);
        }

        // POST: Danhgiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Madanhgia,Masp,Makh,Ngaydanhgia,Loidanhgia,Diemdanhgia,Hinhanh")] Danhgium danhgium)
        {
            if (id != danhgium.Madanhgia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhgium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhgiumExists(danhgium.Madanhgia))
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
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "Makh", "Makh", danhgium.Makh);
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Masp", danhgium.Masp);
            return View(danhgium);
        }

        // GET: Danhgiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhgium = await _context.Danhgia
                .Include(d => d.MakhNavigation)
                .Include(d => d.MaspNavigation)
                .FirstOrDefaultAsync(m => m.Madanhgia == id);
            if (danhgium == null)
            {
                return NotFound();
            }

            return View(danhgium);
        }

        // POST: Danhgiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhgium = await _context.Danhgia.FindAsync(id);
            if (danhgium != null)
            {
                _context.Danhgia.Remove(danhgium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhgiumExists(int id)
        {
            return _context.Danhgia.Any(e => e.Madanhgia == id);
        }
    }
}
