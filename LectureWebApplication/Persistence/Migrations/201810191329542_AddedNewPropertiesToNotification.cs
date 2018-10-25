namespace LectureWebApplication.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewPropertiesToNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "NewDateTime", c => c.DateTime());
            AddColumn("dbo.Notifications", "NewSubject", c => c.String());
            AddColumn("dbo.Notifications", "NewDepartment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "NewDepartment");
            DropColumn("dbo.Notifications", "NewSubject");
            DropColumn("dbo.Notifications", "NewDateTime");
        }
    }
}
