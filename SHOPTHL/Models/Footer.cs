using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHOPTHL.Data;

namespace SHOPTHL.Models
{
	public class Footer: ViewComponent
	{
		private readonly Thlshop2Context _context;

		public Footer(Thlshop2Context context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			return View(_context.Loais.ToList());
		}
	}
}
