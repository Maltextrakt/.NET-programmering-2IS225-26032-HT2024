using Miljoboven.Models.POCO;

namespace Miljoboven.Models.ViewModels
{
    // view model för att hantera vilka ärenden olika roller ser som inloggad
    public class MyErrand
    {
        public DateTime DateOfObservation { get; set; } // datum då observationen av brottet gjordes 
        public int ErrandId { get; set; } // Unikt ID för ärendet
        public string RefNumber { get; set; } // Referensnummer för ärendet (används som unik identifierare)
        public string TypeOfCrime { get; set; } // Typ av brott som ärendet avser, exempelvis "Nedskräpning" eller "Vattenförorening"

        public string StatusName { get; set; } // Namn på ärendets status

        public string StatusId { get; set; } // Statuses unika id

        public string DepartmentName { get; set; } // Namn på den avdelning som hanterar ärendet
        public string EmployeeName { get; set; } // Namn på den handläggare som är tilldelad ärendet

    }
}

