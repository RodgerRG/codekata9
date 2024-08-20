using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;

namespace Code.Kata._9.Data.Entities;

public class PricingInfo
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PricingInfoId { get; set; }
    public int SalesItemId { get; set; }
    public int PricingCatalogueId { get; set; }
    
    public float CostPerUnit { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PricingUnit PricingUnit { get; init; }

    public float? DiscountQuantityThreshold { get; init; }

    public float? DiscountPercentage { get; init; }
    
    //Related Entities
    public SalesItem SalesItem { get; set; }
    public PricingCatalogue PricingCatalogue { get; set; }
    public ICollection<PricingRule> PricingRules { private get; set; }

    public float ComputeCost(float itemQuantity)
    {
        float total = CostPerUnit * itemQuantity;

        foreach (var rule in PricingRules)
        {
            
        }

        return total;
    }
}