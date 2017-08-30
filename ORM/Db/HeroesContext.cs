using System.Data.Entity;
using ORM.Entities;

namespace ORM.Db
{
    public class HeroesContext : DbContext
    {
        static HeroesContext()
        {
            Database.SetInitializer(new HeroesInitializer());
        }

        public HeroesContext() : base("HeroesContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Reward> Rewards { get; set; }
    }
}