using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models.POCO;

namespace Miljoboven.Components
{
    // Viewcomponent för att visa validate-sidan för ett rapporterat brott
    public class ValidateCrimeViewComponent : ViewComponent
    {
        // Metod som anropas för att visa komponenten i vyn
        // Tar emot ett Errand objekt som innehåller uppgifter om miljöbrottet
        public IViewComponentResult Invoke(Errand errand)
        {
            return View(errand);
        }
    }
}
