using Microsoft.AspNetCore.Mvc;

namespace Miljoboven.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }


        public ViewResult Login()
        {
            return View();
        }
    }
       
}
