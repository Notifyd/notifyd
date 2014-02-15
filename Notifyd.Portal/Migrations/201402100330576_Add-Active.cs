namespace Notifyd.Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Active", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Active");
        }
    }
}
