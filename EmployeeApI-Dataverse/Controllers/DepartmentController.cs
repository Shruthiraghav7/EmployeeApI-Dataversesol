using EmployeeApI_Dataverse.Data;
using EmployeeApI_Dataverse.DTO;
using EmployeeApI_Dataverse.Model;
using EmployeeApI_Dataverse.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace EmployeeApI_Dataverse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly DataverseService _dataverseService;

        public DepartmentController(DataverseService dataverseService)
        {
            _dataverseService = dataverseService;
        }

        [HttpPost(Name = "CreateDepartment")]
        public async Task<Guid> CreateDepartment(CreateDept dto)
        {

            var serviceClient = _dataverseService.GetClient();
            var serviceClient11 = _dataverseService.GetClient();

            Entity dept = new Entity("cr9a7_department1");

            var Query = new QueryExpression("cr9a7_department1")
            {
                 ColumnSet= new ColumnSet(true)
            };

            var res= serviceClient.RetrieveMultiple(Query);
            int maxdeptid = res.Entities.Count();

            var deptid = (maxdeptid+1).ToString();

            dept["cr9a7_dept_id"] = deptid;

            dept["cr9a7_department"] = dto.departmentname;

            Guid deptId = serviceClient.Create(dept);

            return deptId;
        }

        [HttpGet(Name = "GetDepartment")]
        public async Task<IActionResult> GetDepartment(int pageNumber, int pageSize)
        {
            var serviceClient = _dataverseService.GetClient();

            var query=new QueryExpression("cr9a7_department1")
            {
                ColumnSet = new ColumnSet(true),
                PageInfo = new PagingInfo()
                {
                    PageNumber = pageNumber,
                    Count = pageSize
                }

            };
            EntityCollection result = serviceClient.RetrieveMultiple(query);

            var department = result.Entities.Select(e => new
            {
              dept_id = e.Contains("cr9a7_dept_id") ? e["cr9a7_dept_id"].ToString() : "",
                deptname = e.Contains("cr9a7_department") ? e["cr9a7_department"].ToString() : ""
            }).ToList();


            /*  var res = serviceClient.RetrieveMultiple(query);
              List<Department> dept = new List<Department>();

              foreach(var entity in res.Entities)
              {
                  Department d = new Department();
                  d.dept_id = int.Parse(entity["cr9a7_dept_id"].ToString());
                  d.departmentname = entity["cr9a7_department"].ToString();
                  dept.Add(d);
              }
               List<Department> dept = department.Select(d => new Department
              {
                  dept_id = int.Parse(d.dept_id),
                  departmentname = d.deptname
              }).ToList();*/
            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                HasMoreRecords = result.MoreRecords,
                Data = department
            });
        }

        [HttpPut("{id}",Name = "UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDTO dto)
        {
            var serviceClient = _dataverseService.GetClient();

            var query = new QueryExpression("cr9a7_department1")
            {
                ColumnSet = new ColumnSet(true)
            };

            query.Criteria.AddCondition(
                "cr9a7_dept_id",
                ConditionOperator.Equal,
                id.ToString()); // or dto.DeptId.ToString()

            var result = serviceClient.RetrieveMultiple(query);

            var department = result.Entities.FirstOrDefault();

            Guid recid=department.Id;

            Entity dept = new Entity("cr9a7_department1");
            dept.Id = recid;

            dept["cr9a7_department"] = dto.departmentname;

            serviceClient.Update(dept);

            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var serviceClient = _dataverseService.GetClient();
            var query = new QueryExpression("cr9a7_department1")
            {
                ColumnSet = new ColumnSet(true)
            };

            query.Criteria.AddCondition(
               "cr9a7_dept_id",
               ConditionOperator.Equal,
               id.ToString()); // or dto.DeptId.ToString()

            var result = serviceClient.RetrieveMultiple(query);

            var department = result.Entities.FirstOrDefault();

            Guid recid = department.Id;
            serviceClient.Delete("cr9a7_department1", recid);
            return NoContent();
        }
    }

}