using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Miljoboven.Models;
using Microsoft.AspNetCore.Hosting;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Miljoboven.Models.POCO;
using System.Text;

namespace Miljoboven.Controllers
{
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
            // hämta errandet från repositoryt
            var errand = errandRepository.GetErrandById(errandId);

            //kolla så att det faktiskt existerar
            if (errand == null)
            {
                return NotFound();
            }

            // uppdatera statusId
            if (!string.IsNullOrEmpty(statusId))
            {
                errand.StatusId = statusId;
            }

            // Lägg till texten (information) till den redan existerande texten utan att overwrita
            if (!string.IsNullOrEmpty(information))
            {
                errand.InvestigatorInfo = (errand.InvestigatorInfo ?? "") + Environment.NewLine + information;
            }

            // Lägg till events texten till den redan existerande texten utan att overwrita
            if (!string.IsNullOrEmpty(events))
            {
                errand.InvestigatorAction = (errand.InvestigatorAction ?? "") + Environment.NewLine + events;
            }

            // hantera filuppladdning 
            // filuppladdning sample
            if (loadSample != null && loadSample.Length > 0)
            {
                // Temporär sökväg
                var uniqueFileName = Path.GetFileNameWithoutExtension(loadSample.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(loadSample.FileName);

                // Skapa ny sökväg i wwwroot för att lagra filen permanent
                var sampleFinalPath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Samples", errandId.ToString());
                if (!Directory.Exists(sampleFinalPath))
                {
                    Directory.CreateDirectory(sampleFinalPath); // Skapa katalogen om den inte finns
                }

                // Den slutgiltiga sökvägen till filen
                var finalSampleFilePath = Path.Combine(sampleFinalPath, uniqueFileName);

                // Spara filen till den slutgiltiga platsen
                using (var stream = new FileStream(finalSampleFilePath, FileMode.Create))
                {
                    await loadSample.CopyToAsync(stream); // Kopiera filen till den slutgiltiga platsen
                }

                // Spara filmetadata i databasen 
                var sample = new Sample
                {
                    SampleName = uniqueFileName,  
                    ErrandId = errandId
                };

                errand.Samples.Add(sample); // Lägg till samplet till ärendet
            }

            // Hantera filuppladdning av bild
            if (loadImage != null && loadImage.Length > 0)
            {
                // Temporär sökväg
                var uniqueFileName = Path.GetFileNameWithoutExtension(loadImage.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(loadImage.FileName);

                // Skapa ny sökväg i wwwroot för att lagra filen permanent
                var imageFinalPath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Pictures", errandId.ToString());

                if (!Directory.Exists(imageFinalPath))
                {
                    Directory.CreateDirectory(imageFinalPath);  // Skapa katalogen om den inte finns
                }

                // Den slutgiltiga sökvägen till filen
                var finalImageFilePath = Path.Combine(imageFinalPath, uniqueFileName);

                // Spara filen till den slutgiltiga platsen
                using (var stream = new FileStream(finalImageFilePath, FileMode.Create))
                {
                    await loadImage.CopyToAsync(stream);  // Kopiera filen till den slutgiltiga platsen
                }

                // Spara filmetadata i databasen 
                var picture = new Picture
                {
                    PictureName = uniqueFileName,  
                    ErrandId = errandId
                };

                errand.Pictures.Add(picture);  // Lägg till bilden till ärendet
            }

            // spara det nu uppdaterade errandet till databasen
            errandRepository.SaveErrand(errand);

            return RedirectToAction("CrimeInvestigator", new {id = errandId});
        }
	}
}
