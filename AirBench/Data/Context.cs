using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirBench.Data
{
    public class Context : DbContext
    {
        public Context() : base("name=AirBenchConnection"){
            Database.SetInitializer<Context>(null);
        }
        public DbSet<Bench> Benches { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}