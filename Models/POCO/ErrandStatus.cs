using System.ComponentModel.DataAnnotations;

namespace Miljoboven.Models.POCO
{
    public class ErrandStatus
    {
        [Key]
        public string StatusId { get; set; }
        public string StatusName { get; set; }
    }

}
