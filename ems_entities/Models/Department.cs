﻿namespace EmployeeManagementSystem.Entities.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Employee HeadOfDepartment { get; set; }
        public List<Employee> TeamLeads { get; set; }
    }
}