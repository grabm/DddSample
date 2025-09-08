namespace DddSample.Application.Exercises.Dto
{
    public sealed record ExerciseDto (Guid Id, string Name, string MuscleGroup, bool IsActive)
    {
    }
}
