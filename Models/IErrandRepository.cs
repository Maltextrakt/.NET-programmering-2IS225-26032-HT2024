using Microsoft.EntityFrameworkCore.Query.Internal;
using Miljoboven.Models.POCO;

namespace Miljoboven.Models
{
    //Interface för att definiera de metoder som ett ErrandRepository måste implementera
    public interface IErrandRepository
    {

        //Create and update
        void SaveErrand(Errand errand);

        //Read
        IQueryable<Errand> Errands { get; }
            
        Errand GetErrandById(int id); //metod för att hämta ett specifikt ärende baserat på ett ID

        //Delete
        Errand DeleteErrand(int errandId);

        //Nya metoder för att hantera business logik
        void AssignInvestigator(int errandId, string employeeId);
        void SetNoAction(int errandId, string reason);
        void UpdateStatus(int errandId, string statusId);
        void AddInformation(int errandId, string information);
        void AddEvents(int errandId, string events);
        void AssignDepartment(int errandId, string departmentId);

        Task AddSampleFileAsync(int errandId, IFormFile sampleFile, IWebHostEnvironment webHostEnvironment);  
        Task AddImageFileAsync(int errandId, IFormFile imageFile, IWebHostEnvironment webHostEnvironment);  

        IQueryable<ErrandStatus> Statuses { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<Department> Departments { get; }
    }

}
