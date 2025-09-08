using DddSample.Application.Common;
using DddSample.Application.Exercises.Dto;
using DddSample.Application.Exercises.Interfaces;
using DddSample.Application.Exercises.Queries;
using DddSample.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.Repositories
{
    internal sealed class ExerciseReadRepository : IExerciseReadRepository
    {
        private readonly AppDbContext _appDbContext;
        public ExerciseReadRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<PagedResult<ExerciseDto>> SearchAsync(ExerciseSearch exerciseSearch, CancellationToken cancellationToken)
        {
            var query = _appDbContext.Exercises.AsNoTracking();

            if (!string.IsNullOrEmpty(exerciseSearch.Text))
            {
                var term = exerciseSearch.Text.Trim();
                query = query.Where(e => e.Name.Contains(term) || e.MuscleGroup.Contains(term));
            }

            query = exerciseSearch.SortBy switch
            {
                ExerciseSortBy.MuscleGroup =>
                    (exerciseSearch.Desc ? query.OrderByDescending(e => e.MuscleGroup)
                            : query.OrderBy(e => e.MuscleGroup)),

                _ =>
                    (exerciseSearch.Desc ? query.OrderByDescending(e => e.Name)
                            : query.OrderBy(e => e.Name)),
            };

            var total = await query.CountAsync(cancellationToken);

            var page = exerciseSearch.Page <=0 ? 1 : exerciseSearch.Page;
            var size = exerciseSearch.PageSize <= 0 ? 50 : exerciseSearch.PageSize;
            var skip = (page - 1) * size;

            var items = await query.Skip(skip).Take(size)
                .Select(e => new ExerciseDto(e.Id, e.Name, e.MuscleGroup, e.IsActive))
            .ToListAsync(cancellationToken);

            var result = new PagedResult<ExerciseDto>(items, total, page, size);

            return result;
        }
    }
}
