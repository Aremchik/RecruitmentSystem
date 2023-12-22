using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentSystem.Model.EmployeesModels.Recruiters
{
    public class Approves : Recruiter
    {
        public int ApproveId { get; set; }
        public string DateAdded { get; set; }
        public string СandidateName { get; set; }
        public string Summary { get; set; }
        public string Contacts { get; set; }
        public string Comment { get; set; }
        public string ApprovedByIT { get; set; }
    }
}
