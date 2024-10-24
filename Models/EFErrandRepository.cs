using Microsoft.EntityFrameworkCore;
using Miljoboven.Models.POCO;
using Miljoboven.Models.ViewModels;
using System.Security.Cryptography;

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

        public IQueryable<Errand> GetErrandsByDepartment(string departmentId)
        {
            return context.Errands
                .Include(e => e.Samples)
                .Include (e => e.Pictures)
                .Where(e => e.DepartmentId == departmentId);
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

        // Assigna investigator och uppdatera status
        public void AssignInvestigator(int errandId, string employeeId)
        {
            var errand = context.Errands.FirstOrDefault(e => e.ErrandId == errandId);
            if (errand != null)
            {
                errand.EmployeeId = employeeId;
                errand.StatusId = "S_A";  // Under utredning (Under investigation)
                errand.InvestigatorInfo = "";
                context.SaveChanges();
            }
        }

        // Sätt ärendet till ingen action
        public void SetNoAction(int errandId, string reason)
        {
            var errand = context.Errands.FirstOrDefault(e => e.ErrandId == errandId);
            if (errand != null)
            {
                errand.StatusId = "S_B";  // Ingen åtgärd (No action)
                errand.InvestigatorInfo = reason;
                errand.EmployeeId = null;  // Ingen handläggare (No investigator)
                context.SaveChanges();
            }
        }

        // Lägg till information utan att skriva över 
        public void AddInformation(int errandId, string information)
        {
            var errand = GetErrandById(errandId);
            if (errand != null && !string.IsNullOrEmpty(information))
            {
                errand.InvestigatorInfo = (errand.InvestigatorInfo ?? "") + Environment.NewLine + information;
                context.SaveChanges();
            }
        }

        // Lägg till events text utan att skriva över
        public void AddEvents(int errandId, string events)
        {
            var errand = GetErrandById(errandId);
            if (errand != null && !string.IsNullOrEmpty(events))
            {
                errand.InvestigatorAction = (errand.InvestigatorAction ?? "") + Environment.NewLine + events;
                context.SaveChanges();
            }
        }

        // Hantera sample filuppladdning
        public async Task AddSampleFileAsync(int errandId, IFormFile sampleFile, IWebHostEnvironment webHostEnvironment)
        {
            var errand = GetErrandById(errandId);
            if (errand != null && sampleFile != null)
            {
                var uniqueFileName = Path.GetFileNameWithoutExtension(sampleFile.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(sampleFile.FileName);
                var sampleFinalPath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Samples", errandId.ToString());
                Directory.CreateDirectory(sampleFinalPath);
                var finalFilePath = Path.Combine(sampleFinalPath, uniqueFileName);

                using (var stream = new FileStream(finalFilePath, FileMode.Create))
                {
                    await sampleFile.CopyToAsync(stream);
                }

                var sample = new Sample
                {
                    SampleName = uniqueFileName,
                    ErrandId = errandId
                };

                errand.Samples.Add(sample);
                context.SaveChanges();
            }
        }

        // Hantera bilduppladdning
        public async Task AddImageFileAsync(int errandId, IFormFile imageFile, IWebHostEnvironment webHostEnvironment)
        {
            var errand = GetErrandById(errandId);
            if (errand != null && imageFile != null)
            {
                var uniqueFileName = Path.GetFileNameWithoutExtension(imageFile.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var imageFinalPath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Pictures", errandId.ToString());
                Directory.CreateDirectory(imageFinalPath);
                var finalFilePath = Path.Combine(imageFinalPath, uniqueFileName);

                using (var stream = new FileStream(finalFilePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                var picture = new Picture
                {
                    PictureName = uniqueFileName,
                    ErrandId = errandId
                };

                errand.Pictures.Add(picture);
                context.SaveChanges();
            }
        }

        public void UpdateStatus(int errandId, string statusId)
        {
            var errand = context.Errands.FirstOrDefault(e => e.ErrandId == errandId);
            if (errand != null)
            {
                errand.StatusId = statusId;
                context.SaveChanges();
            }
        }

        public void AssignDepartment(int errandId, string departmentId)
        {
            var errand = context.Errands.FirstOrDefault(e => e.ErrandId == errandId);
            if (errand != null)
            {
                errand.DepartmentId = departmentId;
                context.SaveChanges();
            }
        }

        public IEnumerable<MyErrand> GetCoordinatorErrands(string statusId, string departmentId, string refnumber)
        {
            var errandList = from err in Errands
                join stat in context.ErrandStatuses on err.StatusId equals stat.StatusId

                join dep in Departments on err.DepartmentId equals dep.DepartmentId
                into departmentErrand from deptE in departmentErrand.DefaultIfEmpty()

                join em in Employees on err.EmployeeId equals em.EmployeeId 
                into employeeErrand from empE in employeeErrand.DefaultIfEmpty()

                orderby err.RefNumber descending

                select new MyErrand
                {
                    DateOfObservation = err.DateOfObservation,
                    ErrandId = err.ErrandId,
                    RefNumber = err.RefNumber,
                    TypeOfCrime = err.TypeOfCrime,
                    StatusName = stat.StatusName,
                    DepartmentName = (err.DepartmentId == null ? "ej tillsatt" : deptE.DepartmentName),
                    EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeeName)
                };

			// Apply status filter if provided
			if (!string.IsNullOrEmpty(statusId) && statusId != "Välj alla")
			{
				errandList = errandList.Where(e => e.StatusName == statusId);
			}

			// Apply department filter if provided
			if (!string.IsNullOrEmpty(departmentId) && departmentId != "Välj alla")
			{
				errandList = errandList.Where(e => e.DepartmentName == departmentId);
			}

			// Apply refnumber filter if provided
			if (!string.IsNullOrEmpty(refnumber))
			{
				errandList = errandList.Where(e => e.RefNumber == refnumber);
			}


			return errandList.ToList();
        }

        // Filtrera errands som en manager ska se (bara de som är för den inloggade managerns department)
        public IEnumerable<MyErrand> GetManagerErrands(string departmentId, string employeeName, string statusId, string refnumber)
        {
            var errandList = from err in Errands
                             join stat in context.ErrandStatuses on err.StatusId equals stat.StatusId
                             join dep in Departments on err.DepartmentId equals dep.DepartmentId
                             into departmentErrand
                             from deptE in departmentErrand.DefaultIfEmpty()
                             join em in Employees on err.EmployeeId equals em.EmployeeId
                             into employeeErrand
                             from empE in employeeErrand.DefaultIfEmpty()
                             where err.DepartmentId == departmentId // filtrera på managerns department
                             orderby err.RefNumber descending
                             select new MyErrand
                             {
                                 DateOfObservation = err.DateOfObservation,
                                 ErrandId = err.ErrandId,
                                 RefNumber = err.RefNumber,
                                 TypeOfCrime = err.TypeOfCrime,
                                 StatusName = stat.StatusName,
                                 DepartmentName = (err.DepartmentId == null ? "ej tillsatt" : deptE.DepartmentName),
                                 EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeeName)
                             };

			// filtrera på status
			if (!string.IsNullOrEmpty(statusId) && statusId != "Välj alla")
			{
				errandList = errandList.Where(e => e.StatusName == statusId);
			}

			// filtrera på investigator
			if (!string.IsNullOrEmpty(employeeName) && employeeName != "Välj alla")
			{
				errandList = errandList.Where(e => e.EmployeeName == employeeName);
			}

			// filtrera på refnumber sökning
			if (!string.IsNullOrEmpty(refnumber))
			{
				errandList = errandList.Where(e => e.RefNumber == refnumber);
			}

			return errandList.ToList();
        }

        //Filtrera investigators på department och roletitle (används för managers)
        public IEnumerable<Employee> GetDepartmentInvestigators(string departmentId)
        {
            return Employees.Where(e => e.DepartmentId == departmentId && e.RoleTitle == "Investigator"); 
        }

        public IQueryable<MyErrand> GetInvestigatorErrands(string employeeId, string statusId, string refnumber)
        {
            var errandList = from err in Errands
                             join stat in context.ErrandStatuses on err.StatusId equals stat.StatusId
                             join dep in Departments on err.DepartmentId equals dep.DepartmentId
                             into departmentErrand
                             from deptE in departmentErrand.DefaultIfEmpty()
                             join em in Employees on err.EmployeeId equals em.EmployeeId
                             into employeeErrand
                             from empE in employeeErrand.DefaultIfEmpty()
                             where err.EmployeeId == employeeId // Only show errands assigned to the logged-in investigator
                             orderby err.RefNumber descending
                             select new MyErrand
                             {
                                 DateOfObservation = err.DateOfObservation,
                                 ErrandId = err.ErrandId,
                                 RefNumber = err.RefNumber,
                                 TypeOfCrime = err.TypeOfCrime,
                                 StatusName = stat.StatusName,
                                 DepartmentName = (err.DepartmentId == null ? "ej tillsatt" : deptE.DepartmentName),
                                 EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeeName)
                             };

			// filtrera på status
			if (!string.IsNullOrEmpty(statusId) && statusId != "Välj alla")
			{
				errandList = errandList.Where(e => e.StatusName == statusId);
			}


			// filtrera på refnumber sökning
			if (!string.IsNullOrEmpty(refnumber))
			{
				errandList = errandList.Where(e => e.RefNumber == refnumber);
			}

			return errandList;
        }
    }
}

