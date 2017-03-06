using Rapid.Data;
using Rapid.Model.Models;
using Rapid.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rapid.Service
{
    public interface IEmployeeManager
    {
        IEnumerable<EmployeeViewModel> GetEmployeeList();
        EmployeeViewModel GetById(int id);
        int Add(EmployeeViewModel employee);
        int Update(EmployeeViewModel employee);
        int Delete(int id);
        int GetEmployeeRepositoryHashCode();

        string AnimalEat(string name);
        string ThrowError();
    }
}
