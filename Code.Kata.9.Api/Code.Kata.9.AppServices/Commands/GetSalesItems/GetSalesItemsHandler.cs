using Code.Kata._9.Api.Responses;
using Code.Kata._9.Data.Repos;
using MediatR;

namespace Code.Kata._9.AppServices.Commands.GetSalesItems;

public class GetSalesItemsHandler(ISalesItemRepo salesItemRepo)
    : IRequestHandler<GetSalesItemsCommand, PaginatedResult<SalesItemResponse>>
{
    private readonly ISalesItemRepo _salesItemRepo = salesItemRepo;

    public async Task<PaginatedResult<SalesItemResponse>> Handle(GetSalesItemsCommand request, CancellationToken cancellationToken)
    {
        var items = await _salesItemRepo.GetPaginatedItems(request.Page, request.PageSize, cancellationToken);
        var resData = new List<SalesItemResponse>();

        foreach (var item in items)
        {
            resData.Add(
                new SalesItemResponse(
                    item.SalesItemId, item.ItemName, item.ItemDescription
                    ));
        }
        
        var response = new PaginatedResult<SalesItemResponse>(resData, request.PageSize, request.Page);
        return response;
    }
}