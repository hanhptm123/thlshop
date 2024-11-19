using Microsoft.AspNetCore.Mvc;
using SHOPTHL.Data;
using System.Linq;

namespace SHOPTHL.Models
{
    public class Thongtinnhanhang : ViewComponent
    {
        private readonly Thlshop2Context _context;

        public Thongtinnhanhang(Thlshop2Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Lấy mã khách hàng từ session
            int? makh = HttpContext.Session.GetInt32("NewCustomerId");

            if (makh.HasValue)
            {
                // Truy vấn cơ sở dữ liệu để lấy đối tượng Khachhang
                var khachhang = _context.Khachhangs.FirstOrDefault(k => k.Makh == makh.Value);

                if (khachhang != null)
                {
                    return View("Default", khachhang);
                }
            }

            // Nếu không tìm thấy mã khách hàng hoặc không tìm thấy khách hàng tương ứng, trả về một giá trị mặc định hoặc xử lý phù hợp
            return Content("Không tìm thấy thông tin khách hàng.");
        }
    }
}
