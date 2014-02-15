namespace Notifyd.Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _210Refactor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "URL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "URL");
        }
    }
}
