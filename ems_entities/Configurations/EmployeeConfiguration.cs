using EmployeeManagementSystem.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.EmployeeNumber);
            builder.HasOne(e => e.Address).WithMany().IsRequired();
            builder.HasOne(e => e.Schedule).WithOne().IsRequired();
            builder.HasOne(e => e.Salary).WithOne().IsRequired();
            builder.HasMany(e => e.WorksInDepartments).WithMany(d => d.WorkerEmployees);
            builder.HasMany(e => e.Functions).WithMany(ef => ef.Employees);
            builder.HasMany(e => e.Records).WithOne();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.PhoneNumber).IsRequired();
            builder.Property(e => e.BankAccountNumber).IsRequired();
            builder.Property(e => e.EmploymentDate).IsRequired();
            builder.Property(e => e.CurrentlyEmployed).IsRequired();
            builder.Property(e => e.TimeFired);
        }
    }
}
