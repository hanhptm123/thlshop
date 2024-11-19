using Microsoft.AspNetCore.Mvc;
using SHOPTHL.Data;

namespace SHOPTHL.Models
{
	public class Ship : ViewComponent
	{
		private readonly Thlshop2Context _context;

		public Ship(Thlshop2Context context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			return View(_context.Ptvanchuyens.ToList());
		}

	}
}
