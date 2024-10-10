using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using System.Threading.Tasks;

namespace Miljoboven.Components
{
    // ViewComponent för att visa detaljer om ett specifikt errand
    public class ErrandViewComponent : ViewComponent
    {
        //Fält för att lagra referensen till IErrandRepository, används för att hämta ärenden
        private readonly IErrandRepository _errandRepository;

        // Konstruktor för att dependency injecta IErrandRepository
        public ErrandViewComponent(IErrandRepository errandRepository)
        {
            _errandRepository = errandRepository;
        }

        // Metod för att hämta och visa ärendedetaljer baserat på errandId
        // köörs asynchront eftersom det kan vara dataintensivt vid användning med riktig databas
        public async Task<IViewComponentResult> InvokeAsync(string errandId)
        {
            var errand = _errandRepository.GetErrandById(errandId);
            return View(errand);
        }
    }
}
