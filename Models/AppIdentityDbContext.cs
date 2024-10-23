using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Miljoboven.Models
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        // konstruktor för att komma åt Identity
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }
    }
}
