using Miljoboven.Models.POCO;

namespace Miljoboven.Models.ViewModels
{
    public class InvestigatorViewModel
    {
        public IEnumerable<MyErrand> Errands { get; set; }
        public IEnumerable<ErrandStatus> Statuses { get; set; }
    }
}
