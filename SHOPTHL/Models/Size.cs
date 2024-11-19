using Microsoft.AspNetCore.Mvc;
using SHOPTHL.Data;

namespace SHOPTHL.Models
{
	public class Size :ViewComponent
	{
		private readonly Thlshop2Context _context;

		public Size(Thlshop2Context context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			return View(_context.Sizes.ToList());
		}
	}
}
