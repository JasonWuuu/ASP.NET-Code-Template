using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rapid.Data;
using Rapid.Model.Models;
using Rapid.Model.ViewModels;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Service.Tests
{
    [TestClass]
    public class EmployeeManagerTest
    {
        private IEmployeeRepository _employeeRepository;
        private EmployeeManager _manager;

        [TestInitialize]
        public void TestInitialize()
        {
            MapConfig.RegisterMappings();
            _employeeRepository = MockRepository.GenerateStub<IEmployeeRepository>();
            _manager = MockRepository.GeneratePartialMock<EmployeeManager>(_employeeRepository);
        }

        [TestMethod, TestCategory("Rapid.Service")]
        public void GetEmployeeList()
        {
            //arrange
            var employeeList = new List<Employee>()
            {
                new Employee()
                {
                    Birthday = new DateTime(2015,12,7),
                    EmployeeName = "^&*, (&^&*(",
                    Id = 1
                },
                new Employee()
                {
                    Birthday = new DateTime(2015,12,8),
                    EmployeeName = "^@@&*, (&^&~!~!*(",
                    Id = 2
                }
            };

            _employeeRepository.Stub(t => t.GetEmployeeList()).Return(employeeList);

            //act
            var actual = _manager.GetEmployeeList();

            //assert
            Assert.AreEqual(new DateTime(2015, 12, 7), actual.First().Birthday);
            Assert.AreEqual(1, actual.First().EmployeeId);
            Assert.AreEqual("^&*", actual.First().LastName);
            Assert.AreEqual("(&^&*(", actual.First().FirstName);
            Assert.AreEqual(new DateTime(2015, 12, 8), actual.Last().Birthday);
            Assert.AreEqual(2, actual.Last().EmployeeId);
            Assert.AreEqual("^@@&*", actual.Last().LastName);
            Assert.AreEqual("(&^&~!~!*(", actual.Last().FirstName);
            _employeeRepository.AssertWasCalled(t => t.GetEmployeeList());
        }

        [TestMethod, TestCategory("Rapid.Service")]
        public void Save_Get()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                EmployeeId = 3
            };

            _manager.save(employeeViewModel, "Get");

            _employeeRepository.AssertWasCalled(t => t.GetById(3));
            
        }

        [TestMethod, TestCategory("Rapid.Service")]
        public void Save_Update()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                EmployeeId = 3
            };

            _manager.save(employeeViewModel, "Update");

            _employeeRepository.AssertWasCalled(t => t.Update(Arg<Employee>.Is.Anything));
            _employeeRepository.AssertWasNotCalled(t => t.Add(Arg<Employee>.Is.Anything));
            _employeeRepository.AssertWasNotCalled(t => t.GetById(3));

        }

        [TestMethod, TestCategory("Rapid.Service")]
        public void Save_Add()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                EmployeeId = 0
            };

            _manager.save(employeeViewModel, "Update");

            _employeeRepository.AssertWasNotCalled(t => t.Update(Arg<Employee>.Is.Anything));
            _employeeRepository.AssertWasCalled(t => t.Add(Arg<Employee>.Is.Anything));
            _employeeRepository.AssertWasNotCalled(t => t.GetById(3));

        }

        [TestMethod, TestCategory("Rapid.Service")]
        public void Save_Delete()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                EmployeeId = 3
            };

            _manager.save(employeeViewModel, "Delete");

            _employeeRepository.AssertWasNotCalled(t => t.GetById(3));
            _employeeRepository.AssertWasCalled(t => t.Delete(3));

        }
    }
}
