using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddSample.Application.Common
{
    public sealed record PagedResult<T>(IReadOnlyList<T> items, int TotalCount, int Page, int PageSize)
    {
    }
}
