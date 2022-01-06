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
    public class SalaryControllerConfiguration : IEntityTypeConfiguration<SalaryController>
    {
        public void Configure(EntityTypeBuilder<SalaryController> builder)
        {
            builder.HasKey(sc => sc.Id);
        }
    }
}
