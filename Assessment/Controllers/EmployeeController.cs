using Assessment.Models;
using Assessment.Repositories;
using Assessment.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Controllers
{
    public class EmployeeController : Controller
    {
        private IRepository<Employee> _repository;
        private IRepository<Department> _repoDepartment;
        private EmployeeEditViewModel editViewModel;
        private IEmployeeRepository _empRepository;

        public EmployeeController()
        {
            this._repository = new Repository<Employee>();
            this._repoDepartment = new Repository<Department>();
            this._empRepository = new EmployeeRepository();
            this.editViewModel = new EmployeeEditViewModel();
        }

        // GET: Employee
        public ActionResult Index(string sortOrder, string searchString)
        {
            IEnumerable<Employee> employee = _empRepository.GetAllEmployeeWithDepartment();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DepartmentSortParm = sortOrder == "Department" ? "department_desc" : "Department";
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                employee = employee.Where(s => s.Name.ToLower().Contains(searchString)
                                         || s.Department.DepartmentName.ToLower().Contains(searchString)
                                        || s.Address.ToLower().Contains(searchString)
                                        || s.ContactNumber.ToString().Contains(searchString)
                                        || s.ShiftTimings.ToLower().Contains(searchString) || 
                                        s.Position.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    employee = employee.OrderByDescending(s => s.Name);
                    break;
                case "Department":
                    employee = employee.OrderBy(s => s.Department.DepartmentName);
                    break;
                case "department_desc":
                    employee = employee.OrderByDescending(s => s.Department.DepartmentName);
                    break;
                default:
                    employee = employee.OrderBy(s => s.Name);
                    break;
            }
           

            return View(employee);

        }

        
        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var employee = _repository.GetById(id);
            return View(employee);
        }

        
        // GET: Employee/Create
        public ActionResult Create()
        {
            getDropDowns();
            editViewModel.JoiningDate = DateTime.Now;
           
            return View(editViewModel);
        }
        



        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmployeeEditViewModel employee, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employee.ImageFileName = uploadFile(file);
                   employee.EmploymentStatus = EmploymentStatus.Active.ToString();
                    Employee emp = mapEmployeeModels(employee, new Employee());
                    _repository.Insert(emp);
                    _repository.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    getDropDowns();
                    return View(editViewModel);
                }
            }
            catch(Exception ex)
            {
                getDropDowns();
                return View(editViewModel);
               
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            editViewModel = mapEmployeeModelsReverse(editViewModel, _repository.GetById(id));
            TempData.Clear();
            if (editViewModel.ImageFileName != null)
              TempData.Add("imagePath", editViewModel.ImageFileName);
            else
                TempData.Add("imagePath","");
            getDropDowns();
            
            return View(editViewModel);
            
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmployeeEditViewModel employee, HttpPostedFileBase file)
        {
          
            try
            {
                if (ModelState.IsValid)
                {
                    if (employee.EndDate != null && employee.EndDate != DateTime.MinValue)
                        employee.EmploymentStatus = EmploymentStatus.Inactive.ToString();
                    else
                        employee.EmploymentStatus = EmploymentStatus.Active.ToString();
                    if (uploadFile(file) != "")
                        employee.ImageFileName = uploadFile(file);
                    else
                        employee.ImageFileName = TempData["imagePath"].ToString();
                    Employee emp = mapEmployeeModels(employee, new Employee());
                    
                    _repository.Update(emp);
                    _repository.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    editViewModel = mapEmployeeModelsReverse(editViewModel, _repository.GetById(id));
                    getDropDowns();

                    return View(editViewModel);
                }
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = _repository.GetById(id);
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var employee = _repository.GetById(id);
                _repository.Delete(id);
                _repository.Save();
                return RedirectToAction("Index");

            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        private Employee mapEmployeeModels(EmployeeEditViewModel empEdit, Employee emp)
        {
            emp.Id = empEdit.Id;
            emp.Name = empEdit.Name;
            emp.Email = empEdit.Email;
            emp.Address = empEdit.Address;
            emp.ContactNumber = empEdit.ContactNumber!=null? Convert.ToInt64(empEdit.ContactNumber) : 0 ;
            emp.EmploymentStatus = empEdit.EmploymentStatus;
            emp.JoiningDate = empEdit.JoiningDate;
            emp.EndDate = empEdit.EndDate != null ? empEdit.EndDate : new DateTime();  
            emp.ShiftTimings = empEdit.ShiftTimings.ToString();
            emp.ImageFileName = empEdit.ImageFileName;
            emp.FavColor = empEdit.FavColor;
            emp.Position = empEdit.Position;
            emp.ParentId = (empEdit.SelectedManager != null || empEdit.SelectedManager != 0) ? empEdit.SelectedManager : null;
            emp.DepartmentId = Convert.ToInt16(empEdit.SelectedDepartment);

            return emp;
        }
        private EmployeeEditViewModel mapEmployeeModelsReverse(EmployeeEditViewModel emp, Employee empEdit)
        {
            emp.Name = empEdit.Name;
            emp.Email = empEdit.Email;
            emp.Address = empEdit.Address;
            emp.ContactNumber = empEdit.ContactNumber;
            emp.EmploymentStatus = empEdit.EmploymentStatus;
            emp.JoiningDate = empEdit.JoiningDate;
            emp.EndDate = empEdit.EndDate;
            emp.ShiftTimings = (ShiftTimings)Enum.Parse(typeof(ShiftTimings), empEdit.ShiftTimings);
            emp.ImageFileName = empEdit.ImageFileName;
            emp.FavColor = empEdit.FavColor;
            emp.Position = empEdit.Position;
            emp.SelectedManager = empEdit.ParentId;
            emp.SelectedDepartment = empEdit.DepartmentId;

            return emp;
        }
      
        private void getDropDowns()
        {
            var departments = _repoDepartment.GetAll();
            editViewModel.Departments = departments.Select(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.DepartmentName
            });
            var managers = _empRepository.GetAllManagers();
            editViewModel.Managers = managers.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            });

        }

        private string uploadFile(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/ProfileImages"),  fileName);
                    file.SaveAs(path);
                   return Path.Combine("~/Content/ProfileImages",  fileName);
                }
               

            }
            return "";
        }
    }
}