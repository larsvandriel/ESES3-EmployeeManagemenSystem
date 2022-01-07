using EmployeeManagementSystem.API.Filters;
using EmployeeManagementSystem.Contracts;
using EmployeeManagementSystem.Entities.Extensions;
using EmployeeManagementSystem.Entities.Models;
using EmployeeManagementSystem.Entities.Parameters;
using EmployeeManagementSystem.Entities.ShapedEntities;
using LoggingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace ems_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly LinkGenerator _linkGenerator;

        public EmployeeController(ILoggerManager logger, IRepositoryWrapper repository, LinkGenerator linkGenerator)
        {
            _logger = logger;
            _repository = repository;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public IActionResult GetEmployees([FromQuery] EmployeeParameters employeeParameters)
        {
            try
            {
                var employees = _repository.Employee.GetAllEmployees(employeeParameters);

                var metadata = new
                {
                    employees.TotalCount,
                    employees.PageSize,
                    employees.CurrentPage,
                    employees.TotalPages,
                    employees.HasNext,
                    employees.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                _logger.LogInfo($"Returned {employees.Count} employees from database.");

                var shapedEmployees = employees.Select(i => i.Entity).ToList();

                var mediaType = (MediaTypeHeaderValue)HttpContext.Items["AcceptHeaderMediaType"];

                if (!mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
                {
                    return Ok(shapedEmployees);
                }

                for (var index = 0; index < employees.Count; index++)
                {
                    var brandLinks = CreateLinksForEmployee(employees[index].Id, employeeParameters.Fields);
                    shapedEmployees[index].Add("Links", brandLinks);
                }

                var employeesWrapper = new LinkCollectionWrapper<Entity>(shapedEmployees);

                return Ok(CreateLinksForEmployees(employeesWrapper));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmployees action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "EmployeeById")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public IActionResult GetEmployeeById(Guid id, [FromQuery] string fields)
        {
            try
            {
                var employee = _repository.Employee.GetEmployeeById(id, fields);

                if (employee.Id == Guid.Empty)
                {
                    _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                var mediaType = (MediaTypeHeaderValue)HttpContext.Items["AcceptHeaderMediaType"];

                if (!mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogInfo($"Returned shaped employee with id: {id}");
                    return Ok(employee.Entity);
                }

                employee.Entity.Add("Links", CreateLinksForEmployee(employee.Id, fields));

                return Ok(employee.Entity);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wring inside GetEmployeeById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee.IsObjectNull())
                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid employee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Employee.CreateEmployee(employee);
                _repository.Save();

                return CreatedAtRoute("EmployeeById", new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(Guid id, [FromBody] Employee employee)
        {
            try
            {
                if (employee.IsObjectNull())
                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid employee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbEmployee = _repository.Employee.GetEmployeeById(id);
                if (dbEmployee.IsEmptyObject())
                {
                    _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Employee.UpdateEmployee(dbEmployee, employee);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            try
            {
                var employee = _repository.Employee.GetEmployeeById(id);
                if (employee.IsEmptyObject())
                {
                    _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Employee.DeleteEmployee(employee);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private IEnumerable<Link> CreateLinksForEmployee(Guid id, string fields = "")
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetEmployeeById), values: new {id, fields}), "self", "GET"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteEmployee), values: new {id}), "delete_employee", "DELETE"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateEmployee), values: new {id}), "update_employee", "PUT")
            };

            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForEmployees(LinkCollectionWrapper<Entity> employeesWrapper)
        {
            employeesWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetEmployees), values: new { }), "self", "GET"));

            return employeesWrapper;
        }
    }
}
