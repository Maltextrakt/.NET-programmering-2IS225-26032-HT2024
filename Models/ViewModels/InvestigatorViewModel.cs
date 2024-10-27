using Miljoboven.Models.POCO;

namespace Miljoboven.Models.ViewModels
{
    // ViewModel för att presentera data som en handläggare (Investigator) behöver se
    public class InvestigatorViewModel
    {
        // Lista över de ärenden som handläggaren ska kunna se och hantera
        public IEnumerable<MyErrand> Errands { get; set; }

        // Lista över statusar för ett ärende
        public IEnumerable<ErrandStatus> Statuses { get; set; }
    }
}
