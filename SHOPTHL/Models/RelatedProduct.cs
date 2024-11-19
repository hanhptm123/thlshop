using Microsoft.AspNetCore.Mvc;
using SHOPTHL.Data;

namespace SHOPTHL.Models
{
	public class RelatedProduct:ViewComponent
	{
		private readonly Thlshop2Context _context;

		public RelatedProduct(Thlshop2Context context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			return View(_context.Sanphams.ToList());
		}
	}
}
