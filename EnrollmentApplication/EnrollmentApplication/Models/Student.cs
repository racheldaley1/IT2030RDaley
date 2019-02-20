using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Student
    {
        [Display(Name = "Student ID")]
        public virtual int StudentId { get; set; }

        [Required (ErrorMessage = "A last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters")]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        [Required(ErrorMessage = "A first name is required")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters")]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }
    }
}