using System.ComponentModel.DataAnnotations;

namespace EmployeeApI_Dataverse.DTO
{
    public class EmployeeDto
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int deptid { get; set; }
    }
}
