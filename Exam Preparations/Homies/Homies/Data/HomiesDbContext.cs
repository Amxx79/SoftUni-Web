using Homies.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyType = Homies.Data.Models.Type;

namespace Homies.Data
{
    public class HomiesDbContext : IdentityDbContext
    {
        public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<MyType> Types{ get; set; }
        public DbSet<EventParticipant> EventsParticipants { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventParticipant>()
                .HasKey(ep => new { ep.EventId, ep.HelperId });

            modelBuilder
                .Entity<MyType>()
                .HasData(new MyType()
                {
                    Id = 1,
                    Name = "Animals"
                },
                new MyType()
                {
                    Id = 2,
                    Name = "Fun"
                },
                new MyType()
                {
                    Id = 3,
                    Name = "Discussion"
                },
                new MyType()
                {
                    Id = 4,
                    Name = "Work"
                });

        }
    }
}