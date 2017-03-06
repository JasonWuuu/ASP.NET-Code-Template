using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Model.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public DateTime? Birthday { get; set; }
        public string NickName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
