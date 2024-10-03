using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using System.Threading.Tasks;

namespace Miljoboven.Components
{
    public class ErrandViewComponent : ViewComponent
    {
        private readonly IErrandRepository _errandRepository;

        public ErrandViewComponent(IErrandRepository errandRepository)
        {
            _errandRepository = errandRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string errandId)
        {
            var errand = _errandRepository.GetErrandById(errandId);
            return View(errand);
        }
    }
}
