using Miljoboven.Models.POCO;

namespace Miljoboven.Models.ViewModels
{
    public class ManagerViewModel
    {
        public IEnumerable<MyErrand> Errands { get; set; }
        public IEnumerable<ErrandStatus> Statuses { get; set; }
        public IEnumerable<Employee> Investigators { get; set; }
        public IEnumerable<Department> Departments { get; set; } 

        public Errand Errand { get; set; } // representerar det valda errandet (som man är i)
        public int ErrandId { get; set; }
    }
}
