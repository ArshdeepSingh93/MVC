using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Assessment.Models;

namespace Assessment.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeManagementContext db;
        private DbSet<Employee> dbSet;

        public EmployeeRepository()
        {
            db = new EmployeeManagementContext();
            dbSet = db.Set<Employee>();
        }

        public IEnumerable<Employee> GetAllEmployeeWithDepartment()
        {

            return db.Employees.Include("Department").ToList();
        }
        IEnumerable<Employee> IEmployeeRepository.GetAll(string searchText)
        {
            return dbSet.Where(x=>x.Name.Contains(searchText)).Select(x => x).ToList();
        }
        IEnumerable<Employee> IEmployeeRepository.GetAllManagers()
        {
            return dbSet.Select(x => x).ToList();
        }
        public void Update(Employee employee)
        {
            
            db.Entry(employee).State = EntityState.Modified;
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

    }
}