using Microsoft.AspNetCore.Mvc;
using SHOPTHL.Data;
using SHOPTHL.Models;
using System.Diagnostics;

namespace SHOPTHL.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Trangchu() { return View(); }
		public IActionResult Shop() { return View(); }
		public IActionResult CtSp() { return View(); }
	
		public IActionResult Privacy()
		{
			return View();
		}
		

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
