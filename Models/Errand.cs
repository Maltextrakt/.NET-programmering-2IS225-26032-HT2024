using System.ComponentModel.DataAnnotations;

namespace Miljoboven.Models
{
    public class Errand
    {
        public string ErrandId { get; set; }
		[Required(ErrorMessage = "Platsen är obligatorisk")]
		public string Place { get; set; }

		[Required(ErrorMessage = "Typ av brott är obligatoriskt")]
		public string TypeOfCrime { get; set; }

		[Required(ErrorMessage = "Datum för observation är obligatoriskt")]
		[DataType(DataType.Date, ErrorMessage = "Ogiltigt datumformat")]
		public DateTime DateOfObservation { get; set; }

		[Required(ErrorMessage = "Ditt namn är obligatoriskt")]
		public string InformerName { get; set; }

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
