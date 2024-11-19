using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHOPTHL.Data;

namespace SHOPTHL.Models
{
    public class DanhGia:ViewComponent
    {
        private readonly Thlshop2Context _context;

        public DanhGia(Thlshop2Context context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int? id)
        {
            var thlshop2Context = _context.Danhgia.Include(d => d.MakhNavigation).Include(d => d.MaspNavigation).Where(s => s.Masp == id).ToList(); // Sử dụng ToList() để đảm bảo dữ liệu đã được load
            return View(thlshop2Context);
        }

    }
}
