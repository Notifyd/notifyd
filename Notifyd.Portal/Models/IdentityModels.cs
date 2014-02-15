using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Notifyd.Portal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public string Name { get; set; }
    //}

    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        [DisplayName("Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }
        [DisplayName("Pushover API Key")]
        public string PushoverKey { get; set; }
        public string Icon { get; set; }
        public bool Active { get; set; }
        [DisplayName("Organization")]
        public Organization UserOrganization { get; set; }

    }

    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<ApplicationUser>
            Members { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
          
        }

        public System.Data.Entity.DbSet<Notifyd.Portal.Models.Notification> Notifications { get; set; }

        public System.Data.Entity.DbSet<Notifyd.Portal.Models.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<Notifyd.Portal.Models.Organization> Organizations { get; set; }

    }
}