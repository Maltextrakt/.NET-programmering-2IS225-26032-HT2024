using Microsoft.EntityFrameworkCore;
using Miljoboven.Models.POCO;

namespace Miljoboven.Models
{
    // Hanterar databasanslutningen och definierar de datamängder (tabeller) som ska användas i applikationen
    public class ApplicationDbContext : DbContext
    {
        // Konstruktor som tar emot DbContextOptions för att konfigurera databasen
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<Department> Departments { get; set; } // Tabell för olika avdelningar i organisationen
        public DbSet<Employee> Employees { get; set; } // Tabell för anställda, inklusive deras roller och avdelningar
        public DbSet<Errand> Errands { get; set; }  // Tabell för ärenden (miljöbrott) som rapporterats i systemet
        public DbSet<ErrandStatus> ErrandStatuses { get; set; } // Tabell för att spåra olika statusar ett ärende kan ha
        public DbSet<Sequence> Sequences { get; set; }  // Tabell för sekvenser, t.ex. för att generera unika ID eller nummerserier
        public DbSet<Sample> Samples { get; set; }  // Tabell för att lagra provtagningar som hör till ärenden
        public DbSet<Picture> Pictures { get; set; } // Tabell för bilder som laddats upp i samband med ärenden


    }
}
