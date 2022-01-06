using EmployeeManagementSystem.Entities.Helpers;
using EmployeeManagementSystem.Entities.Models;
using EmployeeManagementSystem.Entities.Parameters;
using EmployeeManagementSystem.Entities.ShapedEntities;

namespace EmployeeManagementSystem.Contracts
{
    public interface IEmployeeRepository: IRepositoryBase<Employee>
    {
        PagedList<ShapedEntity> GetAllEmployees(EmployeeParameters employeeParameters);
        ShapedEntity GetEmployeeById(Guid employee, string fields);
        Employee GetEmployeeById(Guid departementId);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee dbEmployee, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}