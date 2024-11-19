using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SHOPTHL.Data;

namespace SHOPTHL.Controllers
{
    public class TaikhoansController : Controller
    {
        private readonly Thlshop2Context _context;

        public TaikhoansController(Thlshop2Context context)
        {
            _context = context;
        }

        // GET: Taikhoans
        public async Task<IActionResult> Index()
        {
            return View(await _context.Taikhoans.ToListAsync());
        }

        // GET: Taikhoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans
                .FirstOrDefaultAsync(m => m.Mataikhoan == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return View("Login");
        }
        [HttpPost]

        [HttpPost]
        public IActionResult Login(string Tendangnhap, string Matkhau)
        {
            var taikhoan = _context.Taikhoans.FirstOrDefault(t => t.Tendangnhap == Tendangnhap && t.Matkhau == Matkhau);
            if (taikhoan == null)
            {
                // Xử lý khi tên đăng nhập hoặc mật khẩu không đúng
                return View();
            }

            // Kiểm tra xem khách hàng có mã tài khoản đã có dữ liệu hay không
            var khachhang = _context.Khachhangs.FirstOrDefault(kh => kh.Mataikhoan == taikhoan.Mataikhoan);
            if (khachhang != null)
            {
                // Lưu mã khách hàng vào session
                HttpContext.Session.SetInt32("NewCustomerId", khachhang.Makh);
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, taikhoan.Tendangnhap),
        new Claim(ClaimTypes.Role, taikhoan.Chucvu),
    };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            if (khachhang != null)
            {
                // Nếu có dữ liệu, chuyển hướng đến trang chính (home)
                return RedirectToAction("Index", "Home");
            }

            // Chuyển hướng đến trang tạo khách hàng và truyền mã tài khoản
            return RedirectToAction("Create", "Khachhangs", new { Mataikhoan = taikhoan.Mataikhoan });
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string Tendangnhap, string Matkhau, string Chucvu)
        {
            var taikhoan = new Taikhoan
            {
                Tendangnhap = Tendangnhap,
                Matkhau = Matkhau,
                Chucvu = Chucvu,
                // Thêm các trường khác của bạn ở đây
            };

            _context.Taikhoans.Add(taikhoan);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        // GET: Taikhoans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Taikhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mataikhoan,Tendangnhap,Matkhau,Chucvu")] Taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taikhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taikhoan);
        }

        // GET: Taikhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan == null)
            {
                return NotFound();
            }
            return View(taikhoan);
        }

        // POST: Taikhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mataikhoan,Tendangnhap,Matkhau,Chucvu")] Taikhoan taikhoan)
        {
            if (id != taikhoan.Mataikhoan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taikhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaikhoanExists(taikhoan.Mataikhoan))
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
            return View(taikhoan);
        }

        // GET: Taikhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans
                .FirstOrDefaultAsync(m => m.Mataikhoan == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // POST: Taikhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan != null)
            {
                _context.Taikhoans.Remove(taikhoan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaikhoanExists(int id)
        {
            return _context.Taikhoans.Any(e => e.Mataikhoan == id);
        }
    }
}
