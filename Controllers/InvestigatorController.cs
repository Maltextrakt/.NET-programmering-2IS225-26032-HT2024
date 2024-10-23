using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Miljoboven.Models;
using Microsoft.AspNetCore.Hosting;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Miljoboven.Models.POCO;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Miljoboven.Controllers
{
    [Authorize(Roles = "Investigator")]
	public class InvestigatorController : Controller
	{
        // Fält för att lagra referensen till IErrandRepository, används för att hämta och manipulera errands
        private readonly IErrandRepository errandRepository;
        //lagra referensen till IwebHostEnvironment, används för att routa till wwwroot med filhantering
        private readonly IWebHostEnvironment webHostEnvironment;

        // Konstruktor för att dependency injecta IErrandRepository och IwebHostEnvironment
        public InvestigatorController(IErrandRepository errandRepository, IWebHostEnvironment webHostEnvironment)
		{
			this.errandRepository = errandRepository;
            this.webHostEnvironment = webHostEnvironment;   
		}

		public IActionResult StartInvestigator()
		{
            // Hämtar alla ärenden från repositoryt och skickar dem till vyn
            
            return View(errandRepository);
		}

        // Visar detaljer för ett specifikt ärende baserat på dess ID
        public IActionResult CrimeInvestigator(int id)
		{

            var errand = errandRepository.Errands
                .Include(e => e.Samples)
                .Include(e => e.Pictures)
                .FirstOrDefault(e => e.ErrandId == id);
            
            if (errand == null)
            {
                return NotFound();
            }

            ViewBag.Statuses = errandRepository.Statuses.ToList();
            ViewBag.ErrandId = id; 

            return View(errand);
        }


        // Uppdaterar status, information, händelser, filuppladdning för ett errand
        [HttpPost]
        public async Task<IActionResult> UpdateErrand(int errandId, string statusId, string information, string events, IFormFile loadSample, IFormFile loadImage)
        {

            // uppdatera statusId
            if (!string.IsNullOrEmpty(statusId))
            {
                errandRepository.UpdateStatus(errandId, statusId);
            }

            // Lägg till texten (information) till den redan existerande texten utan att overwrita
            if (!string.IsNullOrEmpty(information))
            {
                errandRepository.AddInformation(errandId, information);
            }

            // Lägg till events texten till den redan existerande texten utan att overwrita
            if (!string.IsNullOrEmpty(events))
            {
                errandRepository.AddEvents(errandId, events);
            }

            // hantera filuppladdning 
            // filuppladdning sample
            if (loadSample != null && loadSample.Length > 0)
            {
                await errandRepository.AddSampleFileAsync(errandId, loadSample, webHostEnvironment);
            }

            // Hantera filuppladdning av bild
            if (loadImage != null && loadImage.Length > 0)
            {
                await errandRepository.AddImageFileAsync(errandId, loadImage, webHostEnvironment);
            }

            return RedirectToAction("CrimeInvestigator", new {id = errandId});
        }
	}
}
