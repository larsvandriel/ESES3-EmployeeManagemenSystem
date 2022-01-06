using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public int EmployeeNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public string BankAccountNumber { get; set; }
        public List<Department> WorksInDepartments { get; set; }
        public List<EmployeeFunction> Functions { get; set; }
        public List<Record> Records { get; set; }
        public Schedule Schedule { get; set; }
        public SalaryController Salary { get; set; }
        public DateTime EmploymentDate { get; set; }
        public bool CurrentlyEmployed { get; set; }
        public DateTime TimeFired { get; set; }
    }
}
