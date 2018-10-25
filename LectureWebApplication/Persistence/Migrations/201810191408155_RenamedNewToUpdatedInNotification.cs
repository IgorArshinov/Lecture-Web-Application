namespace LectureWebApplication.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedNewToUpdatedInNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "UpdatedDateTime", c => c.DateTime());
            AddColumn("dbo.Notifications", "UpdatedSubject", c => c.String());
            AddColumn("dbo.Notifications", "UpdatedDepartment", c => c.String());
            DropColumn("dbo.Notifications", "NewDateTime");
            DropColumn("dbo.Notifications", "NewSubject");
            DropColumn("dbo.Notifications", "NewDepartment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "NewDepartment", c => c.String());
            AddColumn("dbo.Notifications", "NewSubject", c => c.String());
            AddColumn("dbo.Notifications", "NewDateTime", c => c.DateTime());
            DropColumn("dbo.Notifications", "UpdatedDepartment");
            DropColumn("dbo.Notifications", "UpdatedSubject");
            DropColumn("dbo.Notifications", "UpdatedDateTime");
        }
    }
}
