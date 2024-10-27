using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models.ViewModels;
using System.Collections.Generic;

namespace Miljoboven.Components
{
    // viewcomponent för att visa listan med errands för varje inloggad användare
    public class ErrandListViewComponent : ViewComponent
    {
        // Metod som anropas för att visa komponenten i vyn
        // Tar emot en lista med ärenden (errands), samt namnen på controller och action
        public IViewComponentResult Invoke(IEnumerable<MyErrand> errands, string controllerName, string actionName)
        {
            // Sätter ViewBag-variabler för controller och action, används i vyn för att skapa länkar till rätt sida
            ViewBag.actionName = actionName;
            ViewBag.controllerName = controllerName;

            return View(errands);
        }
    }
}
