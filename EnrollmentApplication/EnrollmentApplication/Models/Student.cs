using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnrollmentApplication.Models
{
    public class Student
    {
        public virtual int StudentId { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
    }
}