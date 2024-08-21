namespace Code.Kata._9.Api.Responses;

public class SalesItemResponse(int salesItemId, string itemName, string itemDescription)
{
    public int ItemId { get; } = salesItemId;
    public string ItemName { get; } = itemName;
    public string ItemDescription { get; } = itemDescription;
}