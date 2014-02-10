using Notifyd.Portal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Notifyd.Portal
{
    public class NotifydDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            
            base.Seed(context);

        }

        private void InitializeIdentityForEE(ApplicationDbContext context)
        {

        }
        
    }
    public class IdentityConfig
    {

    }
}