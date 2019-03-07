using Microsoft.AspNetCore.Mvc;

namespace Rolodex.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
