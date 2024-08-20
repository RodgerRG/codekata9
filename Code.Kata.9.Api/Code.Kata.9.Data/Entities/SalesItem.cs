using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Kata._9.Data.Entities;

public class SalesItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SalesItemId { get; set; }

    public string ItemName { get; set; }
    public string ItemDescription { get; set; }

    //Related Entities
    public ICollection<PricingInfo> PricingInfos { get; set; }

    public float ComputeCost(float itemQuantity, int catalogueId)
    {
        var associatedInfo = PricingInfos.FirstOrDefault(pi => pi.PricingCatalogueId == catalogueId);
        if (associatedInfo is null)
            throw new InvalidOperationException("No Pricing Info for this item found in this catalogue");

        return associatedInfo.ComputeCost(itemQuantity);
    }
}