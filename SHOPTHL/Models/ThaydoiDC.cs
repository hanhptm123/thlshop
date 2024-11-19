using Microsoft.AspNetCore.Mvc;
using SHOPTHL.Data;

namespace SHOPTHL.Models
{
    public class ThaydoiDC : ViewComponent
    {
        private readonly Thlshop2Context _context;

        public ThaydoiDC(Thlshop2Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Lấy mã khách hàng từ session
            int? makh = HttpContext.Session.GetInt32("NewCustomerId");

           

            // Nếu không tìm thấy mã khách hàng hoặc không tìm thấy khách hàng tương ứng, trả về một giá trị mặc định hoặc xử lý phù hợp
            return View(_context.Diachis .Where(d => d.Makh == makh).ToList());
        }
    }
}
