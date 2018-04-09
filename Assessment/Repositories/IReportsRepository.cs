using Assessment.Models;
using Assessment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Assessment.Repositories
{
    public interface IReportsRepository
    {
       
        IQueryable<IGrouping<int?, Employee>> GetEmpHiredPerWeek(int week);
        int GetEmpTerminatedInYear(int year);

    }
}