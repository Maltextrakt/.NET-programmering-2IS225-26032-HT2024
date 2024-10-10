namespace Miljoboven.Models
{
    //Interface för att definiera de metoder som ett ErrandRepository måste implementera
    public interface IErrandRepository
    {
        // Metod för att hämta alla ärenden (Errands), ska returnera en lista av ärenden
        IEnumerable<Errand> GetErrands();

        //metod för att hämta ett specifikt ärende baserat på ett ID
        Errand GetErrandById(string id);

        // Metoder för att hämta errandstatusar, employees och departments
        IEnumerable<ErrandStatus> GetErrandStatuses();
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Department> GetDepartments();
    }

}
