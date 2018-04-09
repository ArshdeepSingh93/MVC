using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assessment.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name = "Employee Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "EmailID is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid emailID")]
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Please enter a contact number")]
        [DataType(DataType.PhoneNumber)]
        public Int64 ContactNumber { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Employment Status")]
        public string EmploymentStatus { get; set; }

        [Required]
        [Display(Name = "Shift Timings")]
        public string ShiftTimings { get; set; }

        [Display(Name = "Image")]
        public string ImageFileName { get; set; }

        [Display(Name = "Favorite Color")]
        public string FavColor { get; set; }


        [Display(Name = "Manager Id")]
        public int ?ParentId { get; set; }
        [Display(Name = "Manager")]
        public virtual Employee Parent { get; set; }
        public virtual ICollection<Employee> Children { get; set; }

        [Required]
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }
        [Display(Name = "Department")]
        public virtual Department Department { get; set; }
    }
}