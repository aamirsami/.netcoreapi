using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<ApiResponse> InsertEmployee(Employee employee);
        Task<ApiResponse> UpdateEmployee(Employee employee);
        Task<ApiResponse> DeleteEmployee(int id);
    }
}
