using EmployeeManagementSystem.Contracts;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Entities.Helpers;
using EmployeeManagementSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private RepositoryContext _repoContext;

        private IEmployeeRepository _employee;
        private ISortHelper<Employee> _employeeSortHelper;
        private IDataShaper<Employee> _employeeDataShaper;

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee == null)
                {
                    _employee = new EmployeeRepository(_repoContext, _employeeSortHelper, _employeeDataShaper);
                }

                return _employee;
            }
        }


        public RepositoryWrapper(RepositoryContext repositoryContext, ISortHelper<Employee> employeeSortHelper, IDataShaper<Employee> employeeDataShaper)
        {
            _repoContext = repositoryContext;
            _employeeSortHelper = employeeSortHelper;
            _employeeDataShaper = employeeDataShaper;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
