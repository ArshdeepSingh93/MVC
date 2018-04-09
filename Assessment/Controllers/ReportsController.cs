using Assessment.Extensions;
using Assessment.Repositories;
using Assessment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Controllers
{
    public class ReportsController : Controller
    {
        IReportsRepository _reportsRepository;
        ReportViewModel reportViewModel;
        public ReportsController()
        {
            this._reportsRepository = new ReportsRepository();
            this.reportViewModel = new ReportViewModel();
        }

        [Route("Reports")]
        public ActionResult EmpHiredPerWeek(int week=4)
        {
           var startDate = DateTime.Now;
           var groupedEmpCount= _reportsRepository.GetEmpHiredPerWeek(week);
           var model = groupedEmpCount.Select(x => new
            {
                Key = x.Key,
                EmpCount = x.Count(),
                agg = x.Min(y => y.JoiningDate)

            }).ToList().Select(x => new EmpHiredWeeklyViewModel()
            {
                Key = x.Key,
                EmpCount = x.EmpCount,
                Date = getWeekStartEndDate(x.agg)
            })
               .ToList();
            //GetDate(DateGroupType.Week, x.Key.Value, startDate)
            reportViewModel.weeklyHiredEmployee = model;
            reportViewModel.TerminatedEmpCount = _reportsRepository.GetEmpTerminatedInYear(DateTime.Now.Year);
            ViewBag.weeks = reportViewModel.weeklyHiredEmployee.Max(x=>x.Key);

            return View(reportViewModel);
        }
       
        private string getWeekStartEndDate(DateTime givenDate)
        {
            DateTime StartDate = givenDate.StartOfWeek(DayOfWeek.Monday);
            DateTime EndDate = StartDate.AddDays(7);
            return StartDate.ToShortDateString() + " - " + EndDate.ToShortDateString();
        }

        
        
    }
}