using Rapid.Core.Data;
using Rapid.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private RapidDbContext _context;
        private IDatabase _database;

        public EmployeeRepository(IDatabase database)
        {
            //only in order to create db with entity framework.
            _context = new RapidDbContext();
            _database = database;
        }

        public IEnumerable<Employee> GetEmployeeList()
        {
            var result= _context.Employees.ToList();
            string sql = "select top 100 Id,EmployeeName,Birthday from Employees";
            var command = _database.GetSqlStringCommand(sql);
            return _database.ExecuteAndReturn<Employee>(command);
        }

        public Employee GetById(int id)
        {
            string sql = "select top 100 Id,EmployeeName,Birthday from Employees where Id=@Id";
            var command = _database.GetSqlStringCommand(sql);
            _database.AddParametersFrom(command, new { Id = id });
            return _database.ExecuteAndReturn<Employee>(command).FirstOrDefault();
        }

        public int Add(Employee employee)
        {
            string sql = "insert into Employees(EmployeeName,Birthday)values(@EmployeeName,@Birthday)";
            var command = _database.GetSqlStringCommand(sql);
            _database.AddParametersFrom(command, new
            {
                EmployeeName = employee.EmployeeName,
                Birthday = employee.Birthday
            });

            return _database.ExecuteNonQueryCommand(command);
        }

        public int Update(Employee employee)
        {
            string sql = "update Employees set EmployeeName=@EmployeeName,Birthday=@Birthday where Id=@Id";
            var command = _database.GetSqlStringCommand(sql);
            _database.AddParametersFrom(command, employee);

            return _database.ExecuteNonQueryCommand(command);
        }

        public int Delete(int id)
        {
            string sql = "delete from Employees where Id=@Id";
            var command = _database.GetSqlStringCommand(sql);
            _database.AddParametersFrom(command, new { Id = id });

            return _database.ExecuteNonQueryCommand(command);
        }
    }
}
