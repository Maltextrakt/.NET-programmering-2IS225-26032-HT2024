namespace Miljoboven.Models
{
    //Interface för att definiera de metoder som ett ErrandRepository måste implementera
    public interface IErrandRepository
    {
        
        IQueryable<Errand> Errands { get; }

        //metod för att hämta ett specifikt ärende baserat på ett ID
        Errand GetErrandById(string id);

        
        IQueryable<ErrandStatus> Statuses { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<Department> Departments { get; }
    }

}
