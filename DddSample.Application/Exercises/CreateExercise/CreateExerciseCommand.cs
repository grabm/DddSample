using DddSample.Application.Abstractions;
using DddSample.Application.Exercises.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddSample.Application.Exercises.CreateExercise
{
    public sealed record CreateExerciseCommand (string Name, string MuscleGroup) : ICommand<ExerciseDto>
    {
    }
}
