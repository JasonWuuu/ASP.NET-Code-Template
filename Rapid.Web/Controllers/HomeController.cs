using Rapid.Model.ViewModels;
using Rapid.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rapid.Web.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeManager _employeeManager;

        public HomeController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        public ActionResult Index()
        {
            var employeeList = _employeeManager.GetEmployeeList();

            return View(employeeList);
        }

        public ActionResult Update(int? id)
        {
            var employee = new EmployeeViewModel();

            if ((id ?? 0) != 0)
            {
                employee = _employeeManager.GetById(id.Value);
            }

            return View(employee);
        }

        [HttpPost]
        public ActionResult Update(EmployeeViewModel model)
        {
            if (model.EmployeeId == 0)
            {
                _employeeManager.Add(model);
            }
            else
            {
                _employeeManager.Update(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _employeeManager.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult DependencyInjectionTest()
        {
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();

            list.Add(new KeyValuePair<string, int>("_employeeManager_1", _employeeManager.GetHashCode()));

            var _employeeManager_2 = ServiceLocator.Resolve<IEmployeeManager>();
            list.Add(new KeyValuePair<string, int>("_employeeManager_2", _employeeManager_2.GetHashCode()));

            list.Add(new KeyValuePair<string, int>("employeeRepository_1", _employeeManager.GetEmployeeRepositoryHashCode()));

            list.Add(new KeyValuePair<string, int>("employeeRepository_2", _employeeManager_2.GetEmployeeRepositoryHashCode()));

            return View(list);
        }

        public ActionResult DependencyInjectionDemo(string name)
        {
            var animals = new string[]
            {
                Constants.AnimalType.CAT, 
                Constants.AnimalType.DOG,
                Constants.AnimalType.OTHER 
            };

            ViewBag.Animals = animals;

            string message = string.Empty;

            if (!string.IsNullOrWhiteSpace(name))
            {
                message = _employeeManager.AnimalEat(name);
            }

            return View((object)message);
        }

        public ActionResult WebApiDemo()
        {
            return View();
        }

        public ActionResult ThrowError()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThrowError(int? id)
        {
            _employeeManager.ThrowError();
            return View();
        }
    }
}
