namespace EmployeeManagementSystem.Entities.Models
{
    public class Schedule: IEntity
    {
        public Guid Id { get; set; }
        public Employee Employee { get; set; }
    }
}