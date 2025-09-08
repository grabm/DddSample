using DddSample.Application.Abstractions;
using DddSample.Application.Common;
using DddSample.Application.Exercises.Dto;
using DddSample.Application.Exercises.Queries;

namespace DddSample.Application.Exercises.GetExercises
{
    public sealed record GetExercisesQuery(
        string? Search,
        int Page = 1,
        int PageSize = 50,
        ExerciseSortBy SortBy = ExerciseSortBy.Name,
        bool Desc = false): IQuery<PagedResult<ExerciseDto>>;
}
