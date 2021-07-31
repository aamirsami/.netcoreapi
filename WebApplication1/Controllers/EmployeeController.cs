using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>
        /// List of Employee object (Id, FirstName, MiddleName, LastName)
        /// </returns>
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employeeService.GetEmployees());
        }

        /// <summary>
        /// Inserts employee object into database
        /// </summary>
        /// <param name="emp">Takes Employee emp {string FirstName, string MiddleName, string LastName}</param>
        /// <returns>Returns APIResponse object (Id, Status, Message, Data)</returns>
        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] Employee emp)
        {
            if (emp == null)
                return BadRequest();

            return Ok(await _employeeService.InsertEmployee(emp));
        }
        /// <summary>
        /// Updates employee object into database
        /// </summary>
        /// <param name="emp">Takes Employee emp {Id int, string FirstName, string MiddleName, string LastName}</param>
        /// <returns>Returns APIResponse object (Id, Status, Message, Data)</returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Employee emp)
        {
            if (emp == null)
                return BadRequest();

            return Ok(await _employeeService.UpdateEmployee(emp));
        }
        /// <summary>
        /// Deletes an employee from database
        /// </summary>
        /// <param name="id">Id of employee to be deleted</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            return Ok(await _employeeService.DeleteEmployee(id));
        }

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
    }
}