using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment.ViewModels
{
    public class ReportViewModel
    {
        public List<EmpHiredWeeklyViewModel> weeklyHiredEmployee;
        public int TerminatedEmpCount { get; set; }

    }
}