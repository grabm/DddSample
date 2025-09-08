using DddSample.Domain.Exercises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddSample.Infrastructure.Persistance.Configurations
{
    internal sealed class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("exercises");
            builder.HasKey(e => e.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.MuscleGroup).HasMaxLength(100).IsRequired();

            builder.Ignore(p => p.DomainEvents);
        }
    }
}
