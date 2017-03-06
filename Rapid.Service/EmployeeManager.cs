using AutoMapper;
using Microsoft.Practices.Unity;
using Rapid.Core.Logging;
using Rapid.Data;
using Rapid.Model.Models;
using Rapid.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Service
{
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<EmployeeViewModel> GetEmployeeList()
        {
            var employeeList = _employeeRepository.GetEmployeeList();
            return Mapper.Map<IEnumerable<EmployeeViewModel>>(employeeList);
        }

        public EmployeeViewModel GetById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return Mapper.Map<EmployeeViewModel>(employee);

        }

        public int Add(EmployeeViewModel employeeViewModel)
        {
            var employee = Mapper.Map<Employee>(employeeViewModel);
            return _employeeRepository.Add(employee);
        }

        public int Update(EmployeeViewModel employeeViewModel)
        {
            var employee = Mapper.Map<Employee>(employeeViewModel);
            return _employeeRepository.Update(employee);
        }

        public int Delete(int id)
        {
            return _employeeRepository.Delete(id);
        }

        public int GetEmployeeRepositoryHashCode()
        {
            return _employeeRepository.GetHashCode();
        }

        /// <summary>
        /// Exercise Test
        /// </summary>
        public string AnimalEat(string name)
        {
            return ServiceLocator.Resolve<IAnimalManager>(name).Eat();
        }

        public string ThrowError()
        {
            try
            {
                int i = 0;
                var j = 10 / i;
                return j.ToString();
            }
            catch (Exception ex)
            {
                RapidLogger.Error(ex, "Jason's test error");
                throw ex;
            }
        }

        internal virtual void save(EmployeeViewModel employeeViewModel, string method)
        {
            var employee = Mapper.Map<Employee>(employeeViewModel);

            if (method == "Get")
            {
                _employeeRepository.GetById(employee.Id);
            }
            else if (method == "Update")
            {
                if (employee.Id == 0)
                {
                    _employeeRepository.Add(employee);
                }
                else
                {
                    _employeeRepository.Update(employee);
                }
            }
            else if (method == "Delete")
            {
                _employeeRepository.Delete(employee.Id);
            }
        }
    }
}
