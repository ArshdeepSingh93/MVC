using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Assessment.Models;
using Assessment.ViewModels;
using System.Data.Entity.SqlServer;

namespace Assessment.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private EmployeeManagementContext db;
        private DbSet<Employee> dbSet;

        public ReportsRepository()
        {
            db = new EmployeeManagementContext();
            dbSet = db.Set<Employee>();
        }
        // not used
        //private int Weekly(DateTime day)
        //{
        //    return System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(day, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
        //}

        public IQueryable<IGrouping<int?, Employee>> GetEmpHiredPerWeek(int week)
        {
            
            return db.Employees.GroupBy(x => SqlFunctions.DateDiff("ww", x.JoiningDate, DateTime.Now));
     
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }

      public int GetEmpTerminatedInYear(int year)
        {
            
            return db.Employees.Where(x=>x.EndDate.Year == year).Count();
        }
    }
}