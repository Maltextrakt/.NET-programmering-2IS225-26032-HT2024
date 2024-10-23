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


        IQueryable<ErrandStatus> Statuses { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<Department> Departments { get; }
    }

}
