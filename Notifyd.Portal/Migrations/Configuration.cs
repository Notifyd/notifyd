namespace Notifyd.Portal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Notifyd.Portal.Models;
    using System.Data.SqlClient;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Notifyd.Portal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Notifyd.Portal.Models.ApplicationDbContext context)
        {
            Core.Logs.Logger.LogInfo("Running DB Seed from Config", "Notifyd.Portal");
            SqlConnection.ClearAllPools();
            
            InitializeIdentityForEE(context);

            context.Organizations.AddOrUpdate(p => p.Name,
              new Organization
              {
                  Name = "Notifyd",
                  Description = "Root System",
                  URL = "http://notifyd.net"
              });
            context.Notifications.AddOrUpdate(p => p.Name,
      new Notification
      {
          Name = "Test1 Notification",
          Body = "1234 Main St",
          OrganizationId = 1,
          Subject = "Test 1",
          FromAddress = "noreply@byronpate.com",
          ToAddress = "byronpate@gmail.com",
          FromDisplay = "Notifyd",
          UpdatedBy = "Admin",
          UpdatedOn = System.DateTime.Now
      },
        new Notification
      {
          Name = "Test2 Notification",
          Body = "1234 Main St",
          OrganizationId = 1,
          Subject = "Test 2",
          FromAddress = "noreply@byronpate.com",
          ToAddress = "byronpate@gmail.com",
          FromDisplay = "Notifyd",
          UpdatedBy = "Admin",
          UpdatedOn = System.DateTime.Now
      });
            context.Addresses.AddOrUpdate(p => p.Name,
      new Address
      {
          Name = "Byron Pate",
          Email = "byronpate@gmail.com",
          OrganizationId = 1,
          UpdatedBy = "Admin",
          UpdatedOn = System.DateTime.Now
      });
        }

        private void InitializeIdentityForEE(ApplicationDbContext context)
        {
            Core.Logs.Logger.LogInfo("Running DB Initialize", "Notifyd.Portal");
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //   var myinfo = new MyUserInfo() { FirstName = "Pranav", LastName = "Rastogi" };
            Organization org = new Organization();
            org.Name = "Notifyd";

            string name = "Admin";
            string password = "123456";
            string test = "test";

            //Create Role Test and User Test
            RoleManager.Create(new IdentityRole(test));
            UserManager.Create(new ApplicationUser() { UserName = test });

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new IdentityRole(name));
                Core.Logs.Logger.LogInfo("DB Initialize creaded Role", "Notifyd.Portal");
            }

            //Create User=Admin with password=123456
            var user = new ApplicationUser();
            user.UserName = name;
            user.Email = "byronpate@gmail.com";
            user.MobileNumber = "4046416658";

            org.Owner = user;
          //  user.Organizations.Add(org);

            var adminresult = UserManager.Create(user, password);
            Core.Logs.Logger.LogInfo("DB Initialize created Admin", "Notifyd.Portal");

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }
            Core.Logs.Logger.LogInfo("Completed DB Initialize", "Notifyd.Portal");
        }
    }
}
