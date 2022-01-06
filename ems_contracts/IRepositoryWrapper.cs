using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Contracts
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository Employee { get; }

        void Save();
    }
}
