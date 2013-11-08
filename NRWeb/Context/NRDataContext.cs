using System.Data.Entity;
using NrDataLib.Model;

namespace NRWeb.Context
{
    public class NRDataContext: DbContext
    {
        public DbSet<User> Users;
        public DbSet<Kontigent> Kontigenter;
        public DbSet<Race> Races;
        public DbSet<RaceInstance> RceInstances;
        public DbSet<RaceTime> RaceTimes;


    }
}
