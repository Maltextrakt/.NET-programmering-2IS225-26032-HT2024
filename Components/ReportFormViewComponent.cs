using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Miljoboven.Models.POCO;

namespace Miljoboven.Components
{
    // ViewComponent för att visa formuläret för rapportering av ett miljöbrott
    public class ReportFormViewComponent : ViewComponent
    {
        // Metod som anropas för att rendera komponenten i vyn
        // Tar emot ett `Errand`-objekt (innehåller uppgifter om miljöbrottet) och namnet på den controller som använts
        public IViewComponentResult Invoke(Errand model, string controllerName)
        {
            // spara controllernamnet i VIewbag för att kunna användas i vyn
            ViewBag.ControllerName = controllerName;

            // Returnerar vyn tillsammans med modellen (Errand-objektet) som innehåller formulärets data
            return View(model);
        }
    }
}
