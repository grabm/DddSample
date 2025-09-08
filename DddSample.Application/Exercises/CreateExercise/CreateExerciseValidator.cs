using FluentValidation;

namespace DddSample.Application.Exercises.CreateExercise
{
    public sealed class CreateExerciseValidator : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.MuscleGroup).NotEmpty().MaximumLength(100);
        }
    }
}
