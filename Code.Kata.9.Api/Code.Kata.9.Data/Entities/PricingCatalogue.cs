using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Kata._9.Data.Entities;

public class PricingCatalogue
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PricingCatalogueId { get; set; }
    
    public DateTime InitialisationDate { get; set; }
    
    public TimeSpan CatalogueValidityTime { get; set; }
    
    public ICollection<PricingInfo> PricingInfos { get; set; }
    public ICollection<Checkout> AssociatedCheckouts { get; set; }

    public void ValidatePricingCatalogue()
    {
        var seenSalesItemIds = new List<int>();

        foreach (var pricingInfo in PricingInfos)
        {
            if (seenSalesItemIds.Contains(pricingInfo.SalesItemId))
            {
                throw new InvalidDataException("Pricing catalogue contains duplicate sales items");
            }
            
            seenSalesItemIds.Add(pricingInfo.SalesItemId);
        }
    }
}