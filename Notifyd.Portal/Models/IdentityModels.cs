using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Notifyd.Portal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public string Name { get; set; }
    //}

    public class NotifydUser : IdentityUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string PushoverKey { get; set; }
        public virtual ICollection<Organization>
            Organizations { get; set; }

    }

    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual NotifydUser Owner { get; set; }
        public virtual ICollection<NotifydUser>
            Members { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<NotifydUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<NotifydUser>().ToTable("Users");

        }

        public System.Data.Entity.DbSet<Notifyd.Portal.Models.Notification> Notifications { get; set; }

        public System.Data.Entity.DbSet<Notifyd.Portal.Models.Address> Addresses { get; set; }
    }
}