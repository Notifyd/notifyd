namespace Notifyd.Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrg : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            RenameColumn(table: "dbo.AspNetUserClaims", name: "User_Id", newName: "IdentityUser_Id");
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                        NotifydUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .ForeignKey("dbo.Users", t => t.NotifydUser_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.NotifydUser_Id);
            
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "IsConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "MobileNumber", c => c.String());
            AddColumn("dbo.Users", "PushoverKey", c => c.String());
            AddColumn("dbo.Users", "Organization_Id", c => c.Int());
            AddColumn("dbo.AspNetUserClaims", "UserId", c => c.String());
            AddColumn("dbo.AspNetUserLogins", "IdentityUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserRoles", "IdentityUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUserClaims", "IdentityUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Users", "Organization_Id");
            CreateIndex("dbo.AspNetUserClaims", "IdentityUser_Id");
            CreateIndex("dbo.AspNetUserLogins", "IdentityUser_Id");
            CreateIndex("dbo.AspNetUserRoles", "IdentityUser_Id");
            AddForeignKey("dbo.Users", "Organization_Id", "dbo.Organizations", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.Organizations", "NotifydUser_Id", "dbo.Users");
            DropForeignKey("dbo.Organizations", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Organization_Id", "dbo.Organizations");
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Organizations", new[] { "NotifydUser_Id" });
            DropIndex("dbo.Organizations", new[] { "Owner_Id" });
            DropIndex("dbo.Users", new[] { "Organization_Id" });
            AlterColumn("dbo.AspNetUserClaims", "IdentityUser_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUserRoles", "IdentityUser_Id");
            DropColumn("dbo.AspNetUserLogins", "IdentityUser_Id");
            DropColumn("dbo.AspNetUserClaims", "UserId");
            DropColumn("dbo.Users", "Organization_Id");
            DropColumn("dbo.Users", "PushoverKey");
            DropColumn("dbo.Users", "MobileNumber");
            DropColumn("dbo.Users", "IsConfirmed");
            DropColumn("dbo.Users", "Email");
            DropTable("dbo.Organizations");
            RenameColumn(table: "dbo.AspNetUserClaims", name: "IdentityUser_Id", newName: "User_Id");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "User_Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
        }
    }
}
