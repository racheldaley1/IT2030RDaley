using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EnrollmentApplication.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<EnrollmentDB>
    {
        protected override void Seed(EnrollmentDB context)
        {
            //var enrollments = new List<Enrollment>
            //{
              //  new Enrollment {StudentId = 1, CourseId = 2, Grade = "A", IsActive = true, AssignedCampus = "West", EnrollmentSemester = "Fall", EnrollmentYear = 2017 },
           // }.ForEach(a => context.Enrollments.Add(a));
        }
    }
}