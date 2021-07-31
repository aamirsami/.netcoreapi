using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Service
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeDAL _employeeDAL;
        private IConfiguration _configuration;
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeDAL.GetEmployees();
        }
        public async Task<ApiResponse> InsertEmployee(Employee employee)
        {
            ApiResponse response = new ApiResponse();
            DBResult result = await _employeeDAL.InsertEmployee(employee);
            if (result != null)
            {
                if (result.ID > 0)
                {
                    employee.Id = result.ID;
                    response.Data = employee;
                    response.Message = "Hurray!!! Inserted";
                    response.Status = true;
                }
                else
                {
                    response.Message = "Alas!!! Could not insert.";
                    response.Status = false;
                    response.Data = result.MSG;
                }
            }
            else
            {
                response.Message = "Alas!!! Could not insert.";
                response.Status = false;
            }
            return response;
        }
        public async Task<ApiResponse> UpdateEmployee(Employee employee)
        {
            ApiResponse response = new ApiResponse();
            DBResult result = await _employeeDAL.UpdateEmployee(employee);
            if (result != null)
            {
                if (result.ID > 0)
                {
                    employee.Id = result.ID;
                    response.Data = employee;
                    response.Message = "Hurray!!! Updated";
                    response.Status = true;
                }
                else
                {
                    response.Message = "Alas!!! Could not update.";
                    response.Status = false;
                    response.Data = result.MSG;
                }
            }
            else
            {
                response.Message = "Alas!!! Could not update.";
                response.Status = false;
            }
            return response;
        }
        public async Task<ApiResponse> DeleteEmployee(int id)
        {
            ApiResponse response = new ApiResponse();
            DBResult result = await _employeeDAL.DeleteEmployee(id);
            if (result != null)
            {
                response.Data = result.MSG;
                if (result.ID > 0)
                {
                    response.Message = "Hurray!!! Deleted";
                    response.Status = true;
                }
                else
                {
                    response.Message = "Alas!!! Could not delete.";
                    response.Status = false;
                    response.Data = result.MSG;
                }
            }
            else
            {
                response.Message = "Alas!!! Could not delete.";
                response.Status = false;
            }

            return response;
        }
        public EmployeeService(IEmployeeDAL employeeDAL, IConfiguration configuration)
        {
            _employeeDAL = employeeDAL;
            _configuration = configuration;
        }
    }
}
