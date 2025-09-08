namespace DddSample.Application.Exercises.Queries
{
    public sealed class ExerciseSearch
    {
        public string? Text { get; init; }
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 50;
        public ExerciseSortBy SortBy { get; init; } = ExerciseSortBy.Name;
        public bool Desc { get; init; } = false;
    }

    public enum ExerciseSortBy { Name, MuscleGroup}
}
