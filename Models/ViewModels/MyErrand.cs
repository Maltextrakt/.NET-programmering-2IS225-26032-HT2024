using Miljoboven.Models.POCO;

namespace Miljoboven.Models.ViewModels
{
    // view model för att hantera vilka ärenden olika roller ser som inloggad
    public class MyErrand
    {
        public DateTime DateOfObservation { get; set; }
        public int ErrandId { get; set; }
        public string RefNumber { get; set; }
        public string TypeOfCrime { get; set; }
        public string StatusName { get; set; }
        public string StatusId { get; set; }    
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }
    }
}

