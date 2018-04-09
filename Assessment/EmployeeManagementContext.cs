using Assessment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;


namespace Assessment
{

    public class EmployeeManagementContext : DbContext
    {

        public EmployeeManagementContext() : base("DefaultConnection")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Log> Log { get; set; }
        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
             .Where(p => p.State == EntityState.Modified).ToList();
            
            
            foreach (var change in modifiedEntities)
            {
               
                var OriginalEntities = change.GetDatabaseValues();
            

                var newEntities = change.CurrentValues;

                var entityName = change.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(change);

                foreach (var prop in OriginalEntities.PropertyNames)
                {
            
                    var originalValue = OriginalEntities.GetValue<object>(prop);
                    var currentValue = newEntities.GetValue<object>(prop);
                    Log log = new Log();
                    originalValue = originalValue != null ? originalValue.ToString() : "";
                    currentValue = currentValue != null ? currentValue.ToString() : "";

                    if (!originalValue.Equals(currentValue))
                    {
                        log.LogID = Guid.NewGuid();
                        log.TableName = entityName;
                        log.RecordID = primaryKey.ToString();
                        log.ColumnName = prop;
                        log.OriginalValue = originalValue.ToString();
                        log.Created_date = DateTime.Now;

                        if (change.State == EntityState.Modified)
                        {
                            log.EventType = "Updated";
                            log.NewValue = currentValue.ToString();
                        }
                        else if (change.State == EntityState.Deleted)
                        {
                            log.EventType = "Deleted";
                     
                        }
                        Log.Add(log);

                    }
                }

            }
                return base.SaveChanges();
         

        }

        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
    }
}