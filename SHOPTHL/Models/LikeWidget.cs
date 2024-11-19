using Microsoft.AspNetCore.Mvc;
using SHOPTHL.Data;
using SHOPTHL.Infrastructure;

namespace SHOPTHL.Models
{
    public class LikeWidget : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View(HttpContext.Session.GetJson<Like>("like"));
        }
    }
}