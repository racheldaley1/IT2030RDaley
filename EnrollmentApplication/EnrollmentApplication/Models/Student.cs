using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Student : IValidatableObject
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

        //[MinimumAge(20)]
        [MinimumAge(20)]
        public virtual int Age { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string State { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validation 1: Address 1 and 2 cannot be the same
            if(Address2 != null &&
                    Address2 == Address1)
            {
                yield return (new ValidationResult("Address2 cannot be the same as Address1", new[] { "Address2" }));
            }

            if(State != null &&
                State.Length != 2)
            {
                yield return (new ValidationResult("Enter a 2 digit State code", new[] { "State" }));
            }

            if(Zipcode != null &&
                Zipcode.Length != 5)
            {
                yield return (new ValidationResult("Enter a 5 digit Zipcode", new[] { "Zipcode" }));
            }
            //throw new NotImplementedException();
        }
    }
}