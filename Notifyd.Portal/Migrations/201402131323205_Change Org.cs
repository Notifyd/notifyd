namespace Notifyd.Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Organizations", "ApplicationUser_Id", "dbo.Users");
            DropIndex("dbo.Organizations", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.Users", "UserOrganization_Id", c => c.Int());
            CreateIndex("dbo.Users", "UserOrganization_Id");
            AddForeignKey("dbo.Users", "UserOrganization_Id", "dbo.Organizations", "Id");
            DropColumn("dbo.Organizations", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Organizations", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Users", "UserOrganization_Id", "dbo.Organizations");
            DropIndex("dbo.Users", new[] { "UserOrganization_Id" });
            DropColumn("dbo.Users", "UserOrganization_Id");
            CreateIndex("dbo.Organizations", "ApplicationUser_Id");
            AddForeignKey("dbo.Organizations", "ApplicationUser_Id", "dbo.Users", "Id");
        }
    }
}
