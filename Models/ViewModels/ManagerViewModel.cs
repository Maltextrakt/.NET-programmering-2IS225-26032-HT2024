using Miljoboven.Models.POCO;

namespace Miljoboven.Models.ViewModels
{
    // ViewModel för att presentera data som en chef (Manager) behöver se och hantera
    public class ManagerViewModel
    {
        public IEnumerable<MyErrand> Errands { get; set; } // Lista över ärenden som chefen har behörighet att se
        public IEnumerable<ErrandStatus> Statuses { get; set; } // Lista över tillgängliga statusalternativ för att filtrera ärenden
        public IEnumerable<Employee> Investigators { get; set; } // Lista över handläggare (investigators) som tillhör chefens avdelning
        public IEnumerable<Department> Departments { get; set; } // Lista över alla avdelningar, används för att visa och filtrera baserat på avdelning


        public Errand Errand { get; set; } // representerar det valda errandet (som man är i)
        public int ErrandId { get; set; }  // Id för det valda ärendet, används för att identifiera och hämta specifika ärenden

    }
}
