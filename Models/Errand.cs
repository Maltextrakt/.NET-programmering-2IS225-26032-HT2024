using System.ComponentModel.DataAnnotations;

namespace Miljoboven.Models
{
    // Modellklass för att representera ett Errand
    public class Errand
    {
        public string ErrandId { get; set; }

        // Plats där brottet har inträffat, obligatoriskt fält med valideringsmeddelande
        [Required(ErrorMessage = "Platsen är obligatorisk")]
		public string Place { get; set; }

        // Typ av brott som rapporteras, obligatoriskt fält med valideringsmeddelande
        [Required(ErrorMessage = "Typ av brott är obligatoriskt")]
		public string TypeOfCrime { get; set; }

        // Datum när observationen gjordes, obligatoriskt fält med datumvalidering och felmeddelande
        [Required(ErrorMessage = "Datum för observation är obligatoriskt")]
		[DataType(DataType.Date, ErrorMessage = "Ogiltigt datumformat")]
		public DateTime DateOfObservation { get; set; }

        // Namn på anmälaren, obligatoriskt fält med valideringsmeddelande
        [Required(ErrorMessage = "Ditt namn är obligatoriskt")]
		public string InformerName { get; set; }

        // Telefonnr till anmälaren, obligatoriskt fält med telefonnummervalidering och felmeddelande
        [Required(ErrorMessage = "Ditt telefonnummer är obligatoriskt")]
		[Phone(ErrorMessage = "Ogiltigt telefonnummer")]
		public string InformerPhone { get; set; }

		//Observation är inte nödvändigt i formuläret
		public string Observation { get; set; }

        public string InvestigatorInfo { get; set; }
        public string InvestigatorAction { get; set; }
        public string StatusId { get; set; }
        public string DepartmentId { get; set; }
        public string EmployeeId { get; set; }
    }

}
