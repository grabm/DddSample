using DddSample.Domain.Abstractions;

namespace DddSample.Domain.Exercises
{
    public sealed record ExerciseCreated(Guid ExerciseId, string Name) : IDomainEvent
    {
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    }
}
