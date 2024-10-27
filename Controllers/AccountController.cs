using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Miljoboven.Models.POCO;
using Microsoft.Identity.Client;

namespace Miljoboven.Controllers
{
    // Controller klass för att hantera inloggningskonton, authorize göratt användare bara kommer åt innehåll de tillåts att komma åt
    [Authorize]
    public class AccountController : Controller
    {
        // UserManager och SignInManager används för att hantera användarhantering och inloggning
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        // Konstruktor för att dependency injecta UserManager och SignInManager
        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        // hanterar inloggningsförfrågan, utan att vara inloggad
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        // hanterar inloggningsförfrågan
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            //Validerar modellens data
            if (ModelState.IsValid)
            {
                // Försöker hitta användaren i databasen baserat på användarnamn
                IdentityUser user = await userManager.FindByNameAsync(loginModel.UserName);
                if (user != null)
                {
                    // Loggar ut alla tidigare sessioner för användaren
                    await signInManager.SignOutAsync();
                    // Försöker logga in användaren med det angivna lösenordet
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        // Kontrollera vilken roll användaren har och omdirigera dem till rätt startsida

                        if (await userManager.IsInRoleAsync(user, "Coordinator"))
                        {
                            return Redirect("/Coordinator/StartCoordinator");
                        }

                        if(await userManager.IsInRoleAsync(user, "Manager"))
                        {
                            return Redirect("/Manager/StartManager");
                        }

                        if (await userManager.IsInRoleAsync(user, "Investigator"))
                        {
                            return Redirect("/Investigator/StartInvestigator");
                        }
                    }
                }
            }
            // Om inloggningen misslyckas, lägg till ett felmeddelande och returnera vyn
            ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord.");
            return View(loginModel);
        }
        //Loggar ut användaren
        public async Task<RedirectResult> Logout(string returnUrl="/")
        {
            // Loggar ut användaren och omdirigerar till startsidan
            await signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return Redirect(returnUrl);
        }

        //Visar sidan för nekad åtkomst
        [AllowAnonymous]
        public ViewResult AccessDenied()
        {
            return View();
        }
    }

}
