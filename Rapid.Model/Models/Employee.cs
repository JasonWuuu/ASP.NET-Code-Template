using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Model.Models
{
    [Serializable]
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
