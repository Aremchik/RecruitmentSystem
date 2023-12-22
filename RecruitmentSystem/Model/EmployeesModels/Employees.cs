using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentSystem.Model.EmployeesModels
{
    public class Employees
    {
        public int EmployeesId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Specialization { get; set; }

        public Employees() { }
    }
}
