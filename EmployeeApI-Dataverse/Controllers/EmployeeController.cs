using EmployeeApI_Dataverse.Data;
using EmployeeApI_Dataverse.DTO;
using EmployeeApI_Dataverse.Model;
using EmployeeApI_Dataverse.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Xrm.Sdk;

namespace EmployeeApI_Dataverse.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly DataverseService _dataverseService;

        public EmployeeController(DataverseService dataverseService)
        {
            _dataverseService = dataverseService;
        }

        [HttpPost]
        public async Task<Guid> CreateEmployee(CreateEmployee dto)
        {
            var serviceClient = _dataverseService.GetClient();

            Entity employee = new Entity("cr9a7_employee1");

            employee["cr9a7_name"] = dto.Name;

            employee["cr9a7_email"] = dto.Email;

            //  Guid employeeId = Guid.NewGuid();

            //  employee["cr9a7_id"] = employeeId;

            employee["cr9a7_dept_id"] = 1;

            // new EntityReference("cr9a7_department1", 1);

            // Department Lookup

           // employee["cr9a7_dept_id"] =
    //new EntityReference("cr9a7_department1", departmentGuid);

            Guid employeeId = serviceClient.Create(employee);

            return employeeId;
        }
    }
}

