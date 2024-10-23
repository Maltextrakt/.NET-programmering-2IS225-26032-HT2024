using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miljoboven.Models.POCO
{
    public class Department
    {
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

}
