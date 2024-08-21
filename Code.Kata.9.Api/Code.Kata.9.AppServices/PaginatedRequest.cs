using Code.Kata._9.Api.Responses;
using MediatR;

namespace Code.Kata._9.AppServices;

public abstract class PaginatedRequest<T>(int pageSize, int page) : IRequest<PaginatedResult<T>>
{
    public int PageSize { get; } = pageSize;
    public int Page { get; } = page;
}