using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SHOPTHL.Data;
using System.IO;
using X.PagedList;
using X.PagedList.Mvc.Core;
using X.PagedList.Mvc.Bootstrap4;

namespace SHOPTHL.Controllers
{
	public class SanphamsController : Controller
	{
		private readonly Thlshop2Context _context;
		private readonly IWebHostEnvironment _hostEnvironment;

		public SanphamsController(Thlshop2Context context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_hostEnvironment = webHostEnvironment;
		}
		// GET: Sanphams
		public async Task<IActionResult> Index(int page = 1)
		{
			page = page < 1 ? 1 : page;
				int pagesize = 5;
			var thlshop2Context = _context.Sanphams.Include(s => s.MagiamgiaNavigation).Include(s => s.MaloaiNavigation).Include(s => s.ManccNavigation).Include(s => s.MathNavigation).ToPagedList(page, pagesize);
            return View(thlshop2Context);
		}

        public async Task<IActionResult> Sanphamtheoloaiadmin(int? id)
        {
            var thlshop2Context = _context.Sanphams.Include(s => s.MagiamgiaNavigation).Include(s => s.MaloaiNavigation).Include(s => s.ManccNavigation).Include(s => s.MathNavigation).Where(s => s.Maloai == id);
            return View(await thlshop2Context.ToListAsync());
        }
        public async Task<IActionResult> SpSale()
        {
            var thlshop2Context = _context.Sanphams
                .Include(s => s.MagiamgiaNavigation)
                .Include(s => s.MaloaiNavigation)
                .Include(s => s.ManccNavigation)
                .Include(s => s.MathNavigation)
                .Where(s => s.MagiamgiaNavigation != null && s.MagiamgiaNavigation.Phantramgiam > 0); // Lọc các sản phẩm có phần trăm giảm > 0

            return View(await thlshop2Context.ToListAsync());
        }
        public async Task<IActionResult> SpNew()
        {
            // Tìm mã sản phẩm lớn nhất
            int maxMaSp = await _context.Sanphams.MaxAsync(s => s.Masp);

            var thlshop2Context = await _context.Sanphams
                .Include(s => s.MagiamgiaNavigation)
                .Include(s => s.MaloaiNavigation)
                .Include(s => s.ManccNavigation)
                .Include(s => s.MathNavigation)
                .Where(s => s.Masp >= maxMaSp - 10) // Lấy 10 sản phẩm có mã lớn nhất hoặc gần nhất
                .ToListAsync();

            return View(thlshop2Context);
        }

        public async Task<IActionResult> AdminSearch(string searching, int page = 1)
        {
            if (String.IsNullOrEmpty(searching))
            {
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                var thlshop2Context = _context.Sanphams
                    .Include(s => s.MagiamgiaNavigation)
                    .Include(s => s.MaloaiNavigation)
                    .Include(s => s.ManccNavigation)
                    .Include(s => s.MathNavigation)
                    .ToPagedList(page, pagesize);
                return View(thlshop2Context);
            }
            else
            {
                int pagesize = 8; // You can adjust the page size as needed
                var thlshop2Context = await _context.Sanphams
                    .Include(s => s.MagiamgiaNavigation)
                    .Include(s => s.MaloaiNavigation)
                    .Include(s => s.ManccNavigation)
                    .Include(s => s.MathNavigation)
                    .Where(s => s.Tensp.Contains(searching))
                    .ToPagedListAsync(page, pagesize);
                return View(thlshop2Context);
            }
        }
        public async Task<IActionResult> SpShop(string searching, int page = 1)
		{
			if (String.IsNullOrEmpty(searching))
			{
				page = page < 1 ? 1 : page;
				int pagesize = 8;
				var thlshop2Context = _context.Sanphams
					.Include(s => s.MagiamgiaNavigation)
					.Include(s => s.MaloaiNavigation)
					.Include(s => s.ManccNavigation)
					.Include(s => s.MathNavigation)
					.ToPagedList(page, pagesize);
				return View(thlshop2Context);
			}
			else
			{
				int pagesize = 8; // You can adjust the page size as needed
				var thlshop2Context = await _context.Sanphams
					.Include(s => s.MagiamgiaNavigation)
					.Include(s => s.MaloaiNavigation)
					.Include(s => s.ManccNavigation)
					.Include(s => s.MathNavigation)
					.Where(s => s.Tensp.Contains(searching))
					.ToPagedListAsync(page, pagesize);
				return View(thlshop2Context);
			}
		}


		// GET: Sanphams/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var sanpham = await _context.Sanphams
				.Include(s => s.MagiamgiaNavigation)
				.Include(s => s.MaloaiNavigation)
				.Include(s => s.ManccNavigation)
				.Include(s => s.MathNavigation)
				.FirstOrDefaultAsync(m => m.Masp == id);
			if (sanpham == null)
			{
				return NotFound();
			}

			return View(sanpham);
		}


		public async Task<IActionResult> CtSp(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var sanpham = await _context.Sanphams
				.Include(s => s.MagiamgiaNavigation)
				.Include(s => s.MaloaiNavigation)
				.Include(s => s.ManccNavigation)
				.Include(s => s.MathNavigation)
				.FirstOrDefaultAsync(m => m.Masp == id);
			if (sanpham == null)
			{
				return NotFound();
			}

			return View(sanpham);
		}
		
		public async Task<IActionResult> Sanphamtheoloai(int ?id)
		{
			var thlshop2Context = _context.Sanphams.Include(s => s.MagiamgiaNavigation).Include(s => s.MaloaiNavigation).Include(s => s.ManccNavigation).Include(s => s.MathNavigation).Where(s=>s.Maloai ==id);
			return View(await thlshop2Context.ToListAsync());
		}
        // GET: Sanphams/Create




        [Authorize(Roles = "Administrator")]

		public IActionResult Create()
		{
			ViewData["Magiamgia"] = new SelectList(_context.Giamgia, "Magiamgia", "Phantramgiam");
			ViewData["Maloai"] = new SelectList(_context.Loais, "Maloai", "Tenloai");
			ViewData["Mancc"] = new SelectList(_context.Nhacungcaps, "Mancc", "Tenncc");
			ViewData["Math"] = new SelectList(_context.Thuonghieus, "Math", "Tenth");
			return View();
		}

        // POST: Sanphams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Masp,Tensp,Dongia,Maloai,Mota,Magiamgia,Math,Hinhanh,Mancc,Hinhanh1,Hinhanh2,Hinhanh3,Bangsize")] Sanpham sanpham, IFormFile Hinhanh, IFormFile Hinhanh1, IFormFile Hinhanh2, IFormFile Hinhanh3, IFormFile Bangsize)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;

                // Lưu hình ảnh chính
                if (Hinhanh != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(Hinhanh.FileName);
                    string extension = Path.GetExtension(Hinhanh.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await Hinhanh.CopyToAsync(fileStream);
                    }
                    sanpham.Hinhanh = fileName;
                }

                // Lưu hình ảnh phụ nếu có
                if (Hinhanh1 != null)
                {
                    string fileName1 = Path.GetFileNameWithoutExtension(Hinhanh1.FileName);
                    string extension1 = Path.GetExtension(Hinhanh1.FileName);
                    fileName1 = fileName1 + DateTime.Now.ToString("yymmssfff") + extension1;
                    string path1 = Path.Combine(wwwRootPath + "/Image/", fileName1);
                    using (var fileStream1 = new FileStream(path1, FileMode.Create))
                    {
                        await Hinhanh1.CopyToAsync(fileStream1);
                    }
                    sanpham.Hinhanh1 = fileName1;
                }

                if (Hinhanh2 != null)
                {
                    string fileName2 = Path.GetFileNameWithoutExtension(Hinhanh2.FileName);
                    string extension2 = Path.GetExtension(Hinhanh2.FileName);
                    fileName2 = fileName2 + DateTime.Now.ToString("yymmssfff") + extension2;
                    string path2 = Path.Combine(wwwRootPath + "/Image/", fileName2);
                    using (var fileStream2 = new FileStream(path2, FileMode.Create))
                    {
                        await Hinhanh2.CopyToAsync(fileStream2);
                    }
                    sanpham.Hinhanh2 = fileName2;
                }

                if (Hinhanh3 != null)
                {
                    string fileName3 = Path.GetFileNameWithoutExtension(Hinhanh3.FileName);
                    string extension3 = Path.GetExtension(Hinhanh3.FileName);
                    fileName3 = fileName3 + DateTime.Now.ToString("yymmssfff") + extension3;
                    string path3 = Path.Combine(wwwRootPath + "/Image/", fileName3);
                    using (var fileStream3 = new FileStream(path3, FileMode.Create))
                    {
                        await Hinhanh3.CopyToAsync(fileStream3);
                    }
                    sanpham.Hinhanh3 = fileName3;
                }

                if (Bangsize != null)
                {
                    string fileName4 = Path.GetFileNameWithoutExtension(Bangsize.FileName);
                    string extension4 = Path.GetExtension(Bangsize.FileName);
                    fileName4 = fileName4 + DateTime.Now.ToString("yymmssfff") + extension4;
                    string path4 = Path.Combine(wwwRootPath + "/Image/", fileName4);
                    using (var fileStream4 = new FileStream(path4, FileMode.Create))
                    {
                        await Bangsize.CopyToAsync(fileStream4);
                    }
                    sanpham.Bangsize = fileName4;
                }

                _context.Add(sanpham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Magiamgia"] = new SelectList(_context.Giamgia, "Magiamgia", "Phantramgiam", sanpham.Magiamgia);
            ViewData["Maloai"] = new SelectList(_context.Loais, "Maloai", "Tenloai", sanpham.Maloai);
            ViewData["Mancc"] = new SelectList(_context.Nhacungcaps, "Mancc", "Tenncc", sanpham.Mancc);
            ViewData["Math"] = new SelectList(_context.Thuonghieus, "Math", "Tenth", sanpham.Math);
            return View(sanpham);
        }


        // GET: Sanphams/Edit/5
        [Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var sanpham = await _context.Sanphams.FindAsync(id);
			if (sanpham == null)
			{
				return NotFound();
			}
			ViewData["Magiamgia"] = new SelectList(_context.Giamgia, "Magiamgia", "Phantramgiam", sanpham.Magiamgia);
			ViewData["Maloai"] = new SelectList(_context.Loais, "Maloai", "Tenloai", sanpham.Maloai);
			ViewData["Mancc"] = new SelectList(_context.Nhacungcaps, "Mancc", "Tenncc", sanpham.Mancc);
			ViewData["Math"] = new SelectList(_context.Thuonghieus, "Math", "Tenth", sanpham.Math);
			return View(sanpham);
		}

        // POST: Sanphams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Masp,Tensp,Dongia,Maloai,Mota,Magiamgia,Math,Hinhanh,Mancc,Hinhanh1,Hinhanh2,Hinhanh3,Bangsize")] Sanpham sanpham,
     IFormFile Hinhanh, IFormFile Hinhanh1, IFormFile Hinhanh2, IFormFile Hinhanh3, IFormFile Bangsize)
        {
            if (id != sanpham.Masp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;

                    // Xử lý ảnh chính
                    if (Hinhanh != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(Hinhanh.FileName);
                        string extension = Path.GetExtension(Hinhanh.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await Hinhanh.CopyToAsync(fileStream);
                        }
                        sanpham.Hinhanh = fileName;
                    }

                    // Xử lý ảnh phụ Hinhanh1, Hinhanh2, Hinhanh3
                    if (Hinhanh1 != null)
                    {
                        string fileName1 = Path.GetFileNameWithoutExtension(Hinhanh1.FileName);
                        string extension1 = Path.GetExtension(Hinhanh1.FileName);
                        fileName1 = fileName1 + DateTime.Now.ToString("yymmssfff") + extension1;
                        string path1 = Path.Combine(wwwRootPath + "/Image/", fileName1);
                        using (var fileStream1 = new FileStream(path1, FileMode.Create))
                        {
                            await Hinhanh1.CopyToAsync(fileStream1);
                        }
                        sanpham.Hinhanh1 = fileName1;
                    }

                    if (Hinhanh2 != null)
                    {
                        string fileName2 = Path.GetFileNameWithoutExtension(Hinhanh2.FileName);
                        string extension2 = Path.GetExtension(Hinhanh2.FileName);
                        fileName2 = fileName2 + DateTime.Now.ToString("yymmssfff") + extension2;
                        string path2 = Path.Combine(wwwRootPath + "/Image/", fileName2);
                        using (var fileStream2 = new FileStream(path2, FileMode.Create))
                        {
                            await Hinhanh2.CopyToAsync(fileStream2);
                        }
                        sanpham.Hinhanh2 = fileName2;
                    }

                    if (Hinhanh3 != null)
                    {
                        string fileName3 = Path.GetFileNameWithoutExtension(Hinhanh3.FileName);
                        string extension3 = Path.GetExtension(Hinhanh3.FileName);
                        fileName3 = fileName3 + DateTime.Now.ToString("yymmssfff") + extension3;
                        string path3 = Path.Combine(wwwRootPath + "/Image/", fileName3);
                        using (var fileStream3 = new FileStream(path3, FileMode.Create))
                        {
                            await Hinhanh3.CopyToAsync(fileStream3);
                        }
                        sanpham.Hinhanh3 = fileName3;
                    }

                    // Xử lý ảnh trong bảng size
                    if (Bangsize != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(Bangsize.FileName);
                        string extension = Path.GetExtension(Bangsize.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await Bangsize.CopyToAsync(fileStream);
                        }
                        sanpham.Bangsize = fileName;
                    }

                    _context.Update(sanpham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanphamExists(sanpham.Masp))
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
            ViewData["Magiamgia"] = new SelectList(_context.Giamgia, "Magiamgia", "Phantramgiam", sanpham.Magiamgia);
            ViewData["Maloai"] = new SelectList(_context.Loais, "Maloai", "Tenloai", sanpham.Maloai);
            ViewData["Mancc"] = new SelectList(_context.Nhacungcaps, "Mancc", "Tenncc", sanpham.Mancc);
            ViewData["Math"] = new SelectList(_context.Thuonghieus, "Math", "Tenth", sanpham.Math);
            return View(sanpham);
        }

        // GET: Sanphams/Delete/5
        [Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var sanpham = await _context.Sanphams
				.Include(s => s.MagiamgiaNavigation)
				.Include(s => s.MaloaiNavigation)
				.Include(s => s.ManccNavigation)
				.Include(s => s.MathNavigation)
				.FirstOrDefaultAsync(m => m.Masp == id);
			if (sanpham == null)
			{
				return NotFound();
			}

			return View(sanpham);
		}

		// POST: Sanphams/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var sanpham = await _context.Sanphams.FindAsync(id);
			if (sanpham != null)
			{
				_context.Sanphams.Remove(sanpham);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool SanphamExists(int id)
		{
			return _context.Sanphams.Any(e => e.Masp == id);
		}
	}
}
