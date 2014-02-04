namespace Notifyd.Portal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Notifyd.Portal.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Notifyd.Portal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Notifyd.Portal.Models.ApplicationDbContext context)
        {
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
    }
}
