using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Interfaces
{
    public interface IEmployeeDAL
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<DBResult> InsertEmployee(Employee employee);
        Task<DBResult> UpdateEmployee(Employee employee);
        Task<DBResult> DeleteEmployee(int id);
    }
}
