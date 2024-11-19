using Microsoft.AspNetCore.Mvc;
using SHOPTHL.Data;
using SHOPTHL.Infrastructure;

namespace SHOPTHL.Models
{
	public class CartWidget : ViewComponent
	{
		
		public IViewComponentResult Invoke()
		{
			return View(HttpContext.Session.GetJson<Cart>("cart"));
		}
	}
}
