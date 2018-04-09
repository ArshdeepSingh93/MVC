using Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Assessment.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployeeWithDepartment();
        IEnumerable<Employee> GetAll(string searchString);
        IEnumerable<Employee> GetAllManagers();
        void Update(Employee employee);

    }
}