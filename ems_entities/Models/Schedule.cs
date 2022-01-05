namespace EmployeeManagementSystem.Entities.Models
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public Employee Employee { get; set; }
    }
}