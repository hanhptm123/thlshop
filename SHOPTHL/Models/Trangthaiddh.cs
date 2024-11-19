using Microsoft.AspNetCore.Mvc;
using SHOPTHL.Data;

namespace SHOPTHL.Models
{
    public class Trangthaiddh : ViewComponent
    {

        private readonly Thlshop2Context _context;

        public Trangthaiddh(Thlshop2Context context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Trangthais.ToList());
        }
    }
}
