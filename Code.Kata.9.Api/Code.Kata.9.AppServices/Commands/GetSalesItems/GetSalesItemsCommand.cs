using Code.Kata._9.Api.Responses;
using MediatR;

namespace Code.Kata._9.AppServices.Commands.GetSalesItems;

public class GetSalesItemsCommand(int pageSize, int page) : PaginatedRequest<SalesItemResponse>(pageSize, page)
{
    
}