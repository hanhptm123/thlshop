using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHOPTHL.Data;

namespace SHOPTHL.Models
{
    public class Spdanhgia:ViewComponent
    {
        private readonly Thlshop2Context _context;

        public Spdanhgia(Thlshop2Context context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int? id)
        {
            var thlshop2Context = _context.Sanphams.Include(s => s.MagiamgiaNavigation).Include(s => s.MaloaiNavigation).Include(s => s.ManccNavigation).Include(s => s.MathNavigation).Where(s => s.Masp == id);
            return View(thlshop2Context);
        }
       
    }
}
