namespace EnrollmentApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EnrollmentApplication.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EnrollmentApplication.Models.EnrollmentDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EnrollmentApplication.Models.EnrollmentDB";
        }

        protected override void Seed(EnrollmentApplication.Models.EnrollmentDB context)
        {


        }
    }
}
