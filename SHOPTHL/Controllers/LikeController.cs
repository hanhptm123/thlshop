using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Make sure to include this for HttpContext.Session
using SHOPTHL.Models;
using SHOPTHL.Data;
using SHOPTHL.Infrastructure;
using System.Linq;

namespace SHOPTHL.Controllers
{
    public class LikeController : Controller
    {
        public Like? Like { get; set; }
        private readonly Thlshop2Context _context;

        public LikeController(Thlshop2Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Like", HttpContext.Session.GetJson<Like>("Like"));
        }

        [HttpGet]
        public IActionResult AddToLike(int MASP)
        {
            Sanpham? sanpham = _context.Sanphams.FirstOrDefault(s => s.Masp == MASP);
            if (sanpham != null)
            {
                Like = HttpContext.Session.GetJson<Like>("Like") ?? new Like();
                Like.AddItem(sanpham, 1);
                HttpContext.Session.SetJson("Like", Like);
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true });
            }

            return View("Like", Like);
        }

        public IActionResult UpdateLike(int MASP)
        {
            Sanpham? sanpham = _context.Sanphams.FirstOrDefault(s => s.Masp == MASP);
            if (sanpham != null)
            {
                Like = HttpContext.Session.GetJson<Like>("Like") ?? new Like();

                if (Like.Lines.Any(line => line.Sanpham.Masp == MASP && line.Quantity > 1))
                {
                    Like.AddItem(sanpham, -1);
                    HttpContext.Session.SetJson("Like", Like);
                }
            }
            return View("Like", Like);
        }

        public IActionResult RemoveFromLike(int MASP)
        {
            Sanpham? sanpham = _context.Sanphams.FirstOrDefault(s => s.Masp == MASP);
            if (sanpham != null)
            {
                Like = HttpContext.Session.GetJson<Like>("Like");
                Like.RemoveLine(sanpham);
                HttpContext.Session.SetJson("Like", Like);
            }
            return View("Like", Like);
        }
    }
}
