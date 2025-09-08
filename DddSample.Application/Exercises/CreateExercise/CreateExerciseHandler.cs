using DddSample.Application.Exercises.Dto;
using DddSample.Application.Exercises.Interfaces;
using DddSample.Domain.Exercises;

namespace DddSample.Application.Exercises.CreateExercise
{
    public sealed class CreateExerciseHandler : MediatR.IRequestHandler<CreateExerciseCommand, ExerciseDto>
    {
        private readonly IExerciseRepository _exerciseRepository;
        public CreateExerciseHandler(IExerciseRepository exerciseRepository) => _exerciseRepository = exerciseRepository;

        public async Task<ExerciseDto> Handle(CreateExerciseCommand request, CancellationToken ct)
        {
            var exercise = Exercise.Create(request.Name, request.MuscleGroup);
            await _exerciseRepository.AddAsync(exercise, ct);

            var exerciseDto = new ExerciseDto(exercise.Id, exercise.Name, exercise.MuscleGroup, exercise.IsActive);
            return exerciseDto;
        }
    }
}
