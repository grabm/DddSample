using DddSample.Application.Common;
using DddSample.Application.Exercises.Dto;
using DddSample.Application.Exercises.Queries;

namespace DddSample.Application.Exercises.Interfaces
{
    public interface IExerciseReadRepository
    {
        Task<PagedResult<ExerciseDto>> SearchAsync(ExerciseSearch exerciseSearch, CancellationToken cancellationToken);
    }
}
