using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Course: IValidatableObject
    {
        [Display(Name = "Course ID")]
        public virtual int CourseId { get; set; }

        [Required(ErrorMessage = "An Course Title is required")]
        [StringLength(150, ErrorMessage = "A Course Title cannot be more than 150 characters")]
        [Display(Name = "Course Title")]
        public virtual string Title { get; set; }

        [Display(Name = "Description")]
        public virtual string Description { get; set; }

        [Required(ErrorMessage = "A number of credits is required")]
        [Range(1,4, ErrorMessage = "Course Credits must be a value between 1-4")]
        [Display(Name = "Number of credits")]
        public virtual int Credits { get; set; }

        public virtual string InstructorName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validation 1: Credits have to be between 1-4
            if(Credits <1 || Credits >4)
            {
                yield return (new ValidationResult("Credits must be between 1 and 4"));
            }

            // Validation 2: Description must be 100 words or less
            if(Description.Split(' ').Length > 100)
            {
                yield return (new ValidationResult("Your Description is too verbose"));
            }
            // throw new NotImplementedException();
        }
    }
}