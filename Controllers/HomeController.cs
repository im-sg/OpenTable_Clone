using Microsoft.AspNetCore.Mvc;

namespace OpenTable.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult About()
        {
            return Content("Area: Main, Controller: Home, Action: About");
        }
        public IActionResult Contact()
        {
            return Content("Area: Main, Controller: Home, Action: Contact");
        }
        public IActionResult Privacy()
        {
            return Content("Area: Main, Controller: Home, Action: Privacy");
        }
        public IActionResult Terms()
        {
            return Content("Area: Main, Controller: Home, Action: Terms");
        }
        public IActionResult CookieAds()
        {
            return Content("Area: Main, Controller: Home, Action: CookieAds");
        }
        public IActionResult Mobile()
        {
            return Content("Area: Main, Controller: Home, Action: Mobile");
        }
        public IActionResult Faq()
        {
            return Content("Area: Main, Controller: Home, Action: Faq");
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
