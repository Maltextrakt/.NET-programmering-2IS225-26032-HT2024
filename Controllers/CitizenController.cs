using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
	public class CitizenController : Controller
	{
		public ViewResult Faq()
		{
			return View();
		}

		public ViewResult Contact()
		{
			return View();
		}

		public ViewResult Services()
		{
			return View();

		}

		public ViewResult Thanks()
		{
			return View();
		}
        // Metod för att validera formulärdata från användaren
        // Endast POST-begäran tillåts eftersom det handlar om att skicka data från ett formulär
        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            // Kontrollera om modellen (Errand) inte är giltig baserat på de valideringsregler som finns
            if (!ModelState.IsValid)
            {
                return View("Index", errand);
            }

            return View(errand);
        }


    }
}
