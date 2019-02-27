using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        readonly int minimumAge;
        public MinimumAgeAttribute(int minimumAge)
        {
            this.minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                if ((int)value < minimumAge)
                {
                    return new ValidationResult("Error");
                }
            }
            return ValidationResult.Success;
            //return base.IsValid(value, validationContext);
        }
    }
}