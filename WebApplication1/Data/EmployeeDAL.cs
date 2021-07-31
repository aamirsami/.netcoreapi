using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Data
{
    public class EmployeeDAL : IEmployeeDAL
    {
        //ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString 
        string connstring = @"DBConnection";
        private IConfiguration _configuration;

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString(connstring)))
            {
                String query = "SELECT * FROM EmployeeRecords";
                return await db.QueryAsync<Employee>(query);
            }
        }

        public async Task<DBResult> InsertEmployee(Employee employee)
        {
            DBResult result = null;
            var parameter = new DynamicParameters();
            // parameter.Add("@Id", employee.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameter.Add("@firstname", employee.FirstName);
            parameter.Add("@lastname", employee.LastName);
            parameter.Add("@middlename", employee.MiddleName);
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString(connstring)))
            {
                result = await db.QueryFirstOrDefaultAsync<DBResult>("usp_InsertEmployee", parameter, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<DBResult> UpdateEmployee(Employee employee)
        {
            DBResult result = null;
            var parameter = new DynamicParameters();
            parameter.Add("@Id", employee.Id);
            parameter.Add("@FirstName", employee.FirstName);
            parameter.Add("@LastName", employee.LastName);
            parameter.Add("@MiddleName", employee.MiddleName);
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString(connstring)))
            {
                result = await db.QueryFirstOrDefaultAsync<DBResult>("usp_UpdateEmployee", parameter, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<DBResult> DeleteEmployee(int id)
        {
            DBResult result = null;
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString(connstring)))
            {
                result = await db.QueryFirstOrDefaultAsync<DBResult>("usp_DeleteEmployee", parameter, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public EmployeeDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
