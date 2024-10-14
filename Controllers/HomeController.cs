using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models.POCO;

namespace Miljoboven.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            var errand = new Errand();

            errand.Place = HttpContext.Session.GetString("Place");
            errand.TypeOfCrime = HttpContext.Session.GetString("TypeOfCrime");
            errand.InformerName = HttpContext.Session.GetString("InformerName");
            errand.InformerPhone = HttpContext.Session.GetString("InformerPhone");
            errand.Observation = HttpContext.Session.GetString("Observation");

            var dateString = HttpContext.Session.GetString("DateOfObservation");
            if (!string.IsNullOrEmpty(dateString))
            {
                errand.DateOfObservation = DateTime.Parse(dateString);
            }
            else
            {
                errand.DateOfObservation = DateTime.Today;
            }

            return View(errand);
        }


        public ViewResult Login()
        {
            return View();
        }
    }
       
}
