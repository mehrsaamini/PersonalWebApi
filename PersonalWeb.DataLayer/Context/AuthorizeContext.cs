using Microsoft.EntityFrameworkCore;
using PersonalWeb.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.DataLayer.Context
{
    public class AuthorizeContext:DbContext
    {
        public AuthorizeContext(DbContextOptions<AuthorizeContext> options):base(options)
        {

        }

        #region Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        #endregion
    }
}
