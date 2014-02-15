namespace Notifyd.Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "Description");
        }
    }
}
