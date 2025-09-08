using DddSample.Application.Exercises.Interfaces;
using DddSample.Domain.Exercises;
using DddSample.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.Repositories
{
    internal sealed class ExerciseRepository : IExerciseRepository
    {
        private readonly AppDbContext _appDbContext;

        public ExerciseRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public Task AddAsync(Exercise exercise, CancellationToken ct)
        {
            var result = _appDbContext.Exercises.AddAsync(exercise, ct).AsTask();
            return result;
        }

        public Task<Exercise?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = _appDbContext.Exercises.FirstOrDefaultAsync(x => x.Id == id, ct);
            return result;
        }
    }
}
