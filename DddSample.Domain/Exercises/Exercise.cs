using DddSample.Domain.Abstractions;
using System.Collections.Generic;

namespace DddSample.Domain.Exercises
{
    public sealed class Exercise : Entity
    {
        private Exercise() { } //EF

        public string Name { get; private set; } = default;
        public string MuscleGroup { get; private set; } = default;
        public bool IsActive { get; private set; } = true;

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        private Exercise(string name, string mouscleGroup)
        {
            SetName(name);
            SetGroup(mouscleGroup);
            _domainEvents.Add(new ExerciseCreated(Id, name));
        }

        public static Exercise Create (string name, string muscleGroup) => new Exercise(name, muscleGroup);
        public void Rename (string newName) => SetName(newName);
        public void Deactive() => IsActive = false;

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
            Name = name.Trim();
        }

        private void SetGroup(string group)
        {
            if (string.IsNullOrWhiteSpace(group)) throw new ArgumentException("Group required");
            MuscleGroup = group.Trim();
        }
    }
}
