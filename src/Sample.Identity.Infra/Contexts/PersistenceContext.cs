using Microsoft.EntityFrameworkCore;
using Sample.Identity.Domain.Entities;

namespace Sample.Identity.Infra.Contexts
{
    public class PersistenceContext : DbContext
    {
        public PersistenceContext(DbContextOptions<PersistenceContext> options) : base(options)
        {
        }

        public DbSet<User> Tracking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasNoDiscriminator()
            .HasKey(da => da.Id);
        }
    }
}