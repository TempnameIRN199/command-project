using System;
using System.Data.Entity;
using System.Linq;

namespace Work.EDM
{
    public class WorkContext : DbContext
    {
        public WorkContext(): base("WorkContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<WorkContext>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CV> CVs { get; set; }  
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestCV> RequestCVs { get; set; }
    }   
}