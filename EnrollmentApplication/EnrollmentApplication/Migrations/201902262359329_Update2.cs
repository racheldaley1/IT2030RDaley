namespace EnrollmentApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "InstructorName", c => c.String());
            DropColumn("dbo.Courses", "Professor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Professor", c => c.String());
            DropColumn("dbo.Courses", "InstructorName");
        }
    }
}
