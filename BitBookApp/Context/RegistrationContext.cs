using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace BitBookApp.Context
{
    public class RegistrationContext
    {
       public DbSet<Models.User> Users { set; get; }
       public DbSet<Models.Profile> Profile { set; get;}
    }
}