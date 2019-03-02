using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Enrollment
    {
        public virtual int EnrollmentId { get; set; }
        public virtual int StudentId { get; set; }
        public virtual int CourseId { get; set; }

        [Required (ErrorMessage = "A grade is required")]
        [RegularExpression (@"[A-F]", ErrorMessage = "You must enter a value between A-F")]
        public virtual string Grade { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual bool IsActive { get; set; }

        [Required(ErrorMessage = "An Assigned Campus is required")]
        [Display(Name = "Assigned Campus")]
        public virtual string AssignedCampus { get; set; }

        [Required(ErrorMessage = "An Enrolled Semester is required")]
        [Display(Name = "Enrolled in Semester")]
        public virtual string EnrollmentSemester { get; set; }

        [Required(ErrorMessage = "An Enrolled Year is required")]
        [Range (2018, 2050, ErrorMessage = "You must enter a year of 2018 or later")]
        [Display(Name = "Enrollment Year")]
        public virtual int EnrollmentYear { get; set; }

        [InvalidChars("*")]
        //[InvalidChars("*", ErrorMessage = "Notes cannot have invalid characters")]
        public virtual string Notes { get; set; }
    }
}