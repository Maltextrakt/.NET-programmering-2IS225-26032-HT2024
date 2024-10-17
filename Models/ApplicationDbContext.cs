using Microsoft.EntityFrameworkCore;
using Miljoboven.Models.POCO;

namespace Miljoboven.Models
{
    // Data access, hanterar databaskopplingen
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<Errand> Errands { get; set; }  
        public DbSet<ErrandStatus> ErrandStatuses { get; set; }
        public DbSet<Sequence> Sequences { get; set; }  
        public DbSet<Sample> Samples { get; set; }  
        public DbSet<Picture> Pictures { get; set; }


    }
}
