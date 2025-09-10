using DddSample.Application.Abstractions;
using DddSample.Application.Exercises.Dto;

namespace DddSample.Application.Exercises.CreateExercise
{
    public sealed record CreateExerciseCommand (string Name, string MuscleGroup) : ICommand<ExerciseDto>
    {
    }
}
