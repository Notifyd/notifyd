namespace Notifyd.Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _210RenameUser : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Organizations", name: "NotifydUser_Id", newName: "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Organizations", name: "ApplicationUser_Id", newName: "NotifydUser_Id");
        }
    }
}
