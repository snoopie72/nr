using System.Data.Entity;
using NRWeb.Model;

namespace NRWeb.Context
{
    public class NRDataContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Kontigent> Kontigenter { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<RaceInstance> RceInstances { get; set; }
        public DbSet<RaceTime> RaceTimes { get; set; }


    }
}
