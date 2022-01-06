using EmployeeManagementSystem.Entities.Configurations;
using EmployeeManagementSystem.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Entities
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeFunction> EmployeeFunctions { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<SalaryController> SalaryControllers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeFunctionConfiguration());
            modelBuilder.ApplyConfiguration(new RecordConfiguration());
            modelBuilder.ApplyConfiguration(new SalaryControllerConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleController());
        }
    }
}