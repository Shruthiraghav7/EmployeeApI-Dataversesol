namespace EmployeeApI_Dataverse.Model
{
    public class Department
    {
        public int dept_id { get; set; }
        public string departmentname
        {
            get; set;
        }
        public ICollection<Employee> Employees { get; set; }
        
}
}
