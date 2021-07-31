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

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employeeService.GetEmployees());
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] Employee emp)
        {
            if (emp == null)
                return BadRequest();

            return Ok(await _employeeService.InsertEmployee(emp));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Employee emp)
        {
            if (emp == null)
                return BadRequest();

            return Ok(await _employeeService.UpdateEmployee(emp));
        }

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