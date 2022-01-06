using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities.Parameters
{
    public class EmployeeParameters: QueryStringParameters
    {
        public EmployeeParameters()
        {
            OrderBy = "name";
        }

        public string Name { get; set; }
    }
}
