using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Notifyd.Portal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Notifyd.Portal
{
    public class NotifydDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            Core.Logs.Logger.LogInfo("Running DB Seed from IdentityConfig", "Notifyd.Portal");
           // InitializeIdentityForEE(context);
            base.Seed(context);

        }

       
        
    }

}