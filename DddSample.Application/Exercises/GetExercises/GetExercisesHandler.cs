using DddSample.Application.Common;
using DddSample.Application.Exercises.Dto;
using DddSample.Application.Exercises.Interfaces;
using DddSample.Application.Exercises.Queries;

namespace DddSample.Application.Exercises.GetExercises
{
    public sealed class GetExercisesHandler : MediatR.IRequestHandler<GetExercisesQuery, PagedResult<ExerciseDto>>
    {
        private readonly IExerciseReadRepository _exerciseReadRepository;
        public GetExercisesHandler(IExerciseReadRepository exerciseRepository) => _exerciseReadRepository = exerciseRepository;
        public Task<PagedResult<ExerciseDto>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
        {
            var search = new ExerciseSearch
            {
                Text = string.IsNullOrEmpty(request.Search) ? null : request.Search!.Trim(),
                Page = request.Page <= 0 ? 1 : request.Page,
                PageSize = request.PageSize <= 0 ? 1 : request.PageSize,
                SortBy = request.SortBy,
                Desc = request.Desc
            };

            var result = _exerciseReadRepository.SearchAsync(search, cancellationToken);

            return result;
        }        
    }
}
