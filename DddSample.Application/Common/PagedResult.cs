namespace DddSample.Application.Common
{
    public sealed record PagedResult<T>(IReadOnlyList<T> items, int TotalCount, int Page, int PageSize)
    {
    }
}
