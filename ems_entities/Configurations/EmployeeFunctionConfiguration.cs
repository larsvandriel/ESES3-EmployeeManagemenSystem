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
    public class EmployeeFunctionConfiguration : IEntityTypeConfiguration<EmployeeFunction>
    {
        public void Configure(EntityTypeBuilder<EmployeeFunction> builder)
        {
            builder.HasKey(ef => ef.Id);
            builder.HasMany(ef => ef.Departments).WithMany(d => d.EmployeeFunctions);
            builder.HasMany(ef => ef.Employees).WithMany(e => e.Functions);
            builder.Property(ef => ef.Name);
            builder.Property(ef => ef.Description);
        }
    }
}
