using Miljoboven.Models.POCO;

namespace Miljoboven.Models.ViewModels
{
    public class CoordinatorViewModel
    {
        public IEnumerable<MyErrand> Errands { get; set; } // lista av errands för ett ärende
        public IEnumerable<ErrandStatus> Statuses { get; set; } // lista av statuses för ett ärende
        public IEnumerable<Employee> Employees { get; set; } // lista av employees för ett ärende
        public IEnumerable<Department> Departments { get; set; } // lista av avdelningar för ett ärende


    }
}
