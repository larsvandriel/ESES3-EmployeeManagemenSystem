using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities.Models
{
    public class Record: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }

    }
}
