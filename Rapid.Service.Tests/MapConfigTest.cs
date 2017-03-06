using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using Rapid.Model.Models;
using Rapid.Model.ViewModels;

namespace Rapid.Service.Tests
{
    [TestClass]
    public class MapConfigTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            MapConfig.RegisterMappings();
        }

        [TestMethod, TestCategory("Rapid.Service")]
        public void Map_EmployeeToEmployeeViewModelTest()
        {
            var employee = new Employee()
            {
                Id = 1,
                Birthday = new DateTime(2015, 12, 7),
                EmployeeName = "Jack,Chen"
            };

            var employeeViewModel = new EmployeeViewModel()
            {
                EmployeeId = 1,
                LastName = "Jack",
                FirstName = "Chen",
                Birthday = new DateTime(2015, 12, 7),
            };

            var actual = Mapper.Map<EmployeeViewModel>(employee);

            Assert.AreEqual(employeeViewModel.EmployeeId, actual.EmployeeId);
            Assert.AreEqual(employeeViewModel.LastName, actual.LastName);
            Assert.AreEqual(employeeViewModel.FirstName, actual.FirstName);
            Assert.AreEqual(employeeViewModel.Birthday, actual.Birthday);
            Assert.AreEqual(employeeViewModel.NickName, actual.NickName);
        }

        [TestMethod, TestCategory("Rapid.Service")]
        public void Map_EmployeeViewModelToEmployeeTest()
        {
            var employee = new Employee()
            {
                Id = 1,
                Birthday = new DateTime(2015, 12, 7),
                EmployeeName = "Jack, Chen"
            };

            var employeeViewModel = new EmployeeViewModel()
            {
                EmployeeId = 1,
                LastName = "Jack",
                FirstName = "Chen",
                Birthday = new DateTime(2015, 12, 7),
            };

            var actual = Mapper.Map<Employee>(employeeViewModel);

            Assert.AreEqual(employee.Id, actual.Id);
            Assert.AreEqual(employee.EmployeeName, actual.EmployeeName);
            Assert.AreEqual(employee.Birthday, actual.Birthday);
        }

        [TestMethod, TestCategory("Rapid.Service")]
        public void CombineNameTest()
        {
            string lastName = "Jack";
            string firstName = "Chen";
            var actual = MapConfig.combineName(lastName, firstName);
            Assert.AreEqual("Jack, Chen", actual);
        }

    }
}
