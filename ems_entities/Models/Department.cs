namespace EmployeeManagementSystem.Entities.Models
{
    public class Department: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Employee HeadOfDepartment { get; set; }
        public List<Employee> TeamLeads { get; set; }
        public List<Employee> WorkerEmployees { get; set; }
        public List<EmployeeFunction> EmployeeFunctions { get; set; }
    }
}