namespace EnrollmentApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Professor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Professor");
        }
    }
}
