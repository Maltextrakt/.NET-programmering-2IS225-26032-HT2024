using Microsoft.EntityFrameworkCore;
using Miljoboven.Models.POCO;

namespace Miljoboven.Models
{
    public class EFErrandRepository : IErrandRepository
    {
        private readonly ApplicationDbContext context;

        public EFErrandRepository(ApplicationDbContext ctx)
        {
            this.context = ctx;
        }

        //gamla sättet att hämta errands från db
        //public IQueryable<Errand> Errands => context.Errands;

        public IQueryable<Errand> Errands =>
            context.Errands.Include(e => e.Samples).Include(e => e.Pictures);
        public IQueryable<Department> Departments => context.Departments;
        public IQueryable<Employee> Employees => context.Employees;
        public IQueryable<ErrandStatus> Statuses => context.ErrandStatuses;

        // Metod för att hämta ett specifikt ärende baserat på ärendenummer (ID), eager loading
        public Errand GetErrandById(int id)
        {
            return context.Errands
                .Include(e => e.Samples)
                .Include(e => e.Pictures)
                .FirstOrDefault(e => e.ErrandId == id);
        }
        
        //sparar nya errands eller editar existerande errands
        public void SaveErrand(Errand errand)
        {
            if(errand.ErrandId == 0)
            {

                var sequence = context.Sequences.FirstOrDefault(s => s.Id == 1);
                if (sequence != null)
                {
                    string refNumber = $"{DateTime.Now.Year}-45-{sequence.CurrentValue}";

                    errand.RefNumber = refNumber;  // Set the reference number

                    sequence.CurrentValue++;  // Increment the sequence
                    context.Sequences.Update(sequence);  // Save the sequence update
                }
                else
                {
                    throw new InvalidOperationException("Sequence tablet är inte initialiserat");
                }

                context.Errands.Add(errand);
            } else // om errandet redan existerar (vi vill uppdatera ett errand)
            {
                Errand dbEntry = context.Errands.FirstOrDefault(e => e.ErrandId == errand.ErrandId);
                if(dbEntry != null)
                {
                    //dbEntry.ErrandId = errand.ErrandId;
                    dbEntry.RefNumber = errand.RefNumber;
                    dbEntry.Place = errand.Place;
                    dbEntry.TypeOfCrime = errand.TypeOfCrime;
                    dbEntry.DateOfObservation = errand.DateOfObservation;
                    dbEntry.InformerName = errand.InformerName;
                    dbEntry.InformerPhone = errand.InformerPhone;
                    dbEntry.Observation = errand.Observation;
                    dbEntry.InvestigatorInfo = errand.InvestigatorInfo;
                    dbEntry.InvestigatorAction = errand.InvestigatorAction;
                    dbEntry.StatusId = errand.StatusId;
                    dbEntry.DepartmentId = errand.DepartmentId;
                    dbEntry.EmployeeId = errand.EmployeeId;
                }
            }
            context.SaveChanges();
        }

        //tar bort ett ärende från databasen
        public Errand DeleteErrand(int errandId)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(e => e.ErrandId == errandId);
            if(dbEntry != null)
            {
                context.Errands.Remove(dbEntry);
            }
            context.SaveChanges();
            return dbEntry;
        }
    }
}
