namespace DddSample.Domain.Abstractions
{
    public interface IDomainEvent
    {
        DateTime OccurredOnUtc { get; }
    }
}
