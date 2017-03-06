using Rapid.Model.ViewModels;
using Rapid.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rapid.Web.Controllers
{
    public class ValuesController : ApiController
    {
        private IEmployeeManager _employeeManager;

        public ValuesController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        // GET api/values
        public IEnumerable<EmployeeViewModel> Get()
        {
            return _employeeManager.GetEmployeeList();
        }

        // GET api/values/5
        public EmployeeViewModel Get(int id)
        {
            return _employeeManager.GetById(id);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
