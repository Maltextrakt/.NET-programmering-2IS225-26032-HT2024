using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models.POCO;
using Miljoboven.Infrastructure;

namespace Miljoboven.Controllers
{
    public class HomeController : Controller
    {

        //"startsidan" med formuläret för användare, fylls i med sessionsdata om sådan existerar
        public ViewResult Index()
        {
            var errand = HttpContext.Session.Get<Errand>("CitizenErrand");

            if (errand == null)
            {
                errand = new Errand
                {
                    DateOfObservation = DateTime.Today
                };
            }
            return View(errand);
        }


        public ViewResult Login()
        {
            return View();
        }
    }
       
}
