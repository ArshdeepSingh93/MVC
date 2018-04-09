using Assessment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.ViewModels
{
    public class EmployeeEditViewModel  : IValidatableObject
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
        public Int64 ? ContactNumber { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

       
        [Display(Name = "Employment Status")]
        public string EmploymentStatus { get; set; }

        [Required]
        [Display(Name = "Shift Timings")]
        public ShiftTimings ShiftTimings { get; set; }

        [Display(Name = "Image")]
        public string ImageFileName { get; set; }

        [Display(Name = "Favorite Color")]
        public string FavColor { get; set; }


      
        [Display(Name = "Managers")]
        public int ?SelectedManager { get; set; }
        public IEnumerable<SelectListItem> Managers { get; set; }

        [Required]
        [Display(Name = "Departments")]
        public int SelectedDepartment { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < JoiningDate && EndDate!=DateTime.MinValue)
            {
                yield return
                  new ValidationResult(errorMessage: "End Date must be greater than Joining Date",
                                       memberNames: new[] { "EndDate" });
            }
        }

        
    }
}