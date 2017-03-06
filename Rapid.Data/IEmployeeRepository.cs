using Rapid.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rapid.Data
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployeeList();
        Employee GetById(int id);
        int Add(Employee employee);
        int Update(Employee employee);
        int Delete(int id);
    }
}
