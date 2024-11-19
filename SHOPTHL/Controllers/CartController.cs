using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Make sure to include this for HttpContext.Session
using SHOPTHL.Models;
using SHOPTHL.Data;
using SHOPTHL.Infrastructure;
using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewComponents;


namespace SHOPTHL.Controllers
{
	public class CartController : Controller
	{
		public Cart? Cart { get; set; }
		private readonly Thlshop2Context _context;

		public CartController(Thlshop2Context context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View("Cart", HttpContext.Session.GetJson<Cart>("cart"));
		}
        public IActionResult AddToCart(int MASP)
        {
            Sanpham? sanpham = _context.Sanphams.FirstOrDefault(s => s.Masp == MASP);
            if (sanpham != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(sanpham, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, message = "Đã thêm vào giỏ hàng!" });
            }

            return View("Cart", Cart);
        }


        public IActionResult UpdateCart(int MASP)
		{
			Sanpham? sanpham = _context.Sanphams.FirstOrDefault(s => s.Masp == MASP);
			if (sanpham != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

				// Kiểm tra nếu số lượng của sản phẩm trong giỏ hàng là lớn hơn 1, mới cho phép giảm
				if (Cart.Lines.Any(line => line.Sanpham.Masp == MASP && line.Quantity > 1))
				{
					Cart.AddItem(sanpham, -1);
					HttpContext.Session.SetJson("cart", Cart);
				}
			}
			return View("Cart", Cart);
		}


		public IActionResult RemoveFromCart(int MASP)
		{
			Sanpham? sanpham = _context.Sanphams.FirstOrDefault(s => s.Masp == MASP);
			if (sanpham != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart");
				Cart.RemoveLine(sanpham);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}
		public IActionResult Shopping_success()
		{
			return View();
		}


        private Khachhang GetCustomerInfo(int id)
        {
            return _context.Khachhangs.FirstOrDefault(kh => kh.Makh == id);
        }
        [HttpPost]
        public IActionResult SaveSelectedMaptvc(int selectedMaptvc)
        {
            // Lưu selectedMaptvc vào session
            HttpContext.Session.SetInt32("selectedMaptvc", selectedMaptvc);
            return Ok();
        }
        [HttpPost]
        public IActionResult SaveSelectedAddress(int selectedAddressId)
        {
            // Lưu selectedAddressId vào session
            HttpContext.Session.SetInt32("selectedAddressId", selectedAddressId);
            return Ok();
        }
      
        public IActionResult Checkout()
        {
            try
            {
                Cart cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                if (cart == null || !cart.Lines.Any())
                {
                    // trả về thông báo cho người dùng biết giỏ hàng đang trống và không thể thực hiện thanh toán
                    return Content("Không thể thực hiện thanh toán vì giỏ hàng đang trống.");
                }

                // Lấy mã khách hàng từ session
                int? customerId = HttpContext.Session.GetInt32("NewCustomerId");

                // Kiểm tra xem mã khách hàng có tồn tại không
                if (!customerId.HasValue)
                {
                    return Content("Không tìm thấy thông tin khách hàng.");
                }

                // Lấy thông tin khách hàng từ cơ sở dữ liệu
                Khachhang customer = _context.Khachhangs.FirstOrDefault(kh => kh.Makh == customerId);

                // Kiểm tra xem thông tin khách hàng có tồn tại không
                if (customer == null)
                {
                    return Content("Không tìm thấy thông tin khách hàng.");
                }

                // Lấy giá trị maptvc từ session
                int? selectedMaptvc = HttpContext.Session.GetInt32("selectedMaptvc");

                // Kiểm tra xem maptvc đã được chọn chưa
                if (!selectedMaptvc.HasValue)
                {
                    return Content("Vui lòng chọn phương thức vận chuyển.");
                }
                // Lấy mã địa chỉ đã chọn từ session
                int? selectedAddressId = HttpContext.Session.GetInt32("selectedAddressId");

                // Kiểm tra xem mã địa chỉ đã chọn có tồn tại và khác 0 không
                if (selectedAddressId.HasValue && selectedAddressId.Value != 0)
                {
                    // Lấy thông tin địa chỉ từ cơ sở dữ liệu
                    Diachi address = _context.Diachis.FirstOrDefault(d => d.Madiachi == selectedAddressId);

                    // Kiểm tra xem địa chỉ có tồn tại không
                    if (address != null)
                    {
                        // Thay đổi thông tin đặt hàng thành thông tin địa chỉ mới
                        customer.Tenkh = address.Tennguoinhan;
                        customer.Sdt = address.Sdt;
                        customer.Diachi = address.Diachi1;
                    }
                }
             
                // Tạo đơn đặt hàng mới
                Dondathang order = new Dondathang
                {
                    Ngaydh = DateTime.Now,
                    Hoten = customer.Tenkh, // Điền thông tin khách hàng vào form
                    Sdt = customer.Sdt, // Điền thông tin khách hàng vào form
                    Diachi = customer.Diachi, // Điền thông tin khách hàng vào form
                    Tongtien = cart.ComputeTotalValue(),
                    Matrangthai = 1,
                    Maptvc = selectedMaptvc, // Sử dụng giá trị maptvc từ session
                    Makh = customer.Makh // Gán mã khách hàng cho đơn hàng
                };

                // Thêm đơn đặt hàng vào cơ sở dữ liệu
                _context.Dondathangs.Add(order);
                _context.SaveChanges();

                // Lưu lại Maddh của đơn đặt hàng
                int orderId = order.Maddh;

                // Thêm các chi tiết đơn đặt hàng vào cơ sở dữ liệu
                foreach (var item in cart.Lines)
                {
                    Ctddh orderDetail = new Ctddh
                    {
                        Maddh = orderId,
                        Masp = item.Sanpham.Masp,
                        Soluong = item.Quantity,
                        Thanhtien = item.Sanpham.Dongia * item.Quantity
                    };

                    _context.Ctddhs.Add(orderDetail);
                }

                _context.SaveChanges();

                // Xóa giỏ hàng sau khi đã thanh toán thành công
                cart.Clear();
                HttpContext.Session.SetJson("cart", cart);

                // Chuyển hướng đến action Dondathangtheomakh và truyền id của khách hàng
                return RedirectToAction("Dondathangtheomakh", "Dondathangs");
            }
            catch
            {
                // Xử lý lỗi
                return Content("Lỗi đặt hàng. Vui lòng thử lại");
            }
        }











    }
}