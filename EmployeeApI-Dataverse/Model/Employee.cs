using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApI_Dataverse.Model
{
    public class Employee
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int deptid { get; set; }

        [ForeignKey("deptid")]
        public Department Department { get; set; }



    }
}
