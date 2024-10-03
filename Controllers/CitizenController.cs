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

        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", errand);
            }

            return View(errand);
        }


    }
}
