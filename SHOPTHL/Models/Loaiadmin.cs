using Microsoft.AspNetCore.Mvc;
using SHOPTHL.Data;

namespace SHOPTHL.Models
{
	public class Loaiadmin:ViewComponent
	{
		private readonly Thlshop2Context _context;

		public Loaiadmin(Thlshop2Context context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			return View(_context.Loais.ToList());
		}
	}
}
