using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentSystem.Model.EmployeesModels.ITSpecialists
{
    public class ITSpecialist : Employees
    {
        public int ApprovedResume { get; set; }

        public ITSpecialist()
        {
            ApprovedResume = 0;
        }
    }
}
