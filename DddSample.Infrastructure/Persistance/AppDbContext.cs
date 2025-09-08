using DddSample.Domain.Exercises;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DddSample.Infrastructure.Persistance
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //load all configurations for IEntityTypeConfiguration<> from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
