using Microsoft.EntityFrameworkCore.Query.Internal;
using Miljoboven.Models.POCO;
using Miljoboven.Models.ViewModels;

namespace Miljoboven.Models
{
    //Interface för att definiera de metoder som ett ErrandRepository måste implementera
    public interface IErrandRepository
    {

        //Create and update
        void SaveErrand(Errand errand);

        //Read
        IQueryable<Errand> Errands { get; } // Hämtar alla ärenden i databasen

        Errand GetErrandById(int id); //metod för att hämta ett specifikt ärende baserat på ett ID

        IQueryable<MyErrand> GetInvestigatorErrands(string employeeId, string statusId, string refnumber); // Hämtar ärenden för en specifik handläggare (investigator), filtrerat på ID, status och referensnummer

        IQueryable<Errand> GetErrandsByDepartment(string departmentId); // Hämtar alla ärenden för ett specifikt department

        IEnumerable<MyErrand> GetCoordinatorErrands(string statusId, string departmentId, string refnumber); // Hämtar ärenden för en samordnare (coordinator), med möjlighet att filtrera på status, avdelning och referensnummer

        IEnumerable<MyErrand> GetManagerErrands(string departmentId, string employeeName, string statusId, string refnumber); // Hämtar ärenden för en chef (manager), med möjlighet att filtrera på avdelning, handläggare, status och referensnummer

        IEnumerable<Employee> GetDepartmentInvestigators(string departmentId); // Hämtar alla handläggare (investigators) inom en specifik avdelning


        //Delete
        Errand DeleteErrand(int errandId);

        //Nya metoder för att hantera business logik
        void AssignInvestigator(int errandId, string employeeId); // Tilldelar en handläggare till ett ärende
        void SetNoAction(int errandId, string reason); // Sätter ärendet till "ingen åtgärd" och anger orsak
        void UpdateStatus(int errandId, string statusId); // Uppdaterar status på ett ärende
        void AddInformation(int errandId, string information); // Lägger till ytterligare information till ett ärende
        void AddEvents(int errandId, string events); // Lägger till nya händelser utan att skriva över befintliga
        void AssignDepartment(int errandId, string departmentId); // Tilldelar ett ärende till en avdelning

        // Hantering av bilduppladdning och filhantering för ärenden
        Task AddSampleFileAsync(int errandId, IFormFile sampleFile, IWebHostEnvironment webHostEnvironment);  
        Task AddImageFileAsync(int errandId, IFormFile imageFile, IWebHostEnvironment webHostEnvironment);

        // Iqueyrables för att hämta alla statusar, anställda och avdelningar
        IQueryable<ErrandStatus> Statuses { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<Department> Departments { get; }
    }

}
