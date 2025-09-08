using DddSample.Domain.Exercises;

namespace DddSample.Application.Exercises.Interfaces
{
    public interface IExerciseRepository
    {
        Task AddAsync(Exercise exercise, CancellationToken ct);
        Task<Exercise?> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
