using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Code.Kata._9.Data.Entities;

public class PricingInfo
{
    //Default constructor for EF Core
    public PricingInfo()
    {
    }

    public PricingInfo(int salesItemId, int pricingCatalogueId, float defaultCostPerUnit, string pricingUnitName)
    {
        AlternatePricing = new List<PricingRule>();
        SalesItemId = salesItemId;
        PricingCatalogueId = pricingCatalogueId;
        DefaultCostPerUnit = defaultCostPerUnit;
        PricingUnitName = pricingUnitName;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PricingInfoId { get; set; }

    public int SalesItemId { get; set; }
    public int PricingCatalogueId { get; set; }
    public float DefaultCostPerUnit { get; set; }

    public string PricingUnitName { get; set; }

    //Related Entities
    public SalesItem SalesItem { get; set; }
    public PricingCatalogue PricingCatalogue { get; set; }
    public ICollection<PricingRule> AlternatePricing { get; set; }

    public float ComputeCost(float itemQuantity)
    {
        float total = DefaultCostPerUnit * itemQuantity;

        foreach (var rule in AlternatePricing)
        {
            //we want to take the lowest possible total based off special pricing
            float alternateTotal = rule.ComputeCost(DefaultCostPerUnit, itemQuantity);
            if (alternateTotal < total)
            {
                total = alternateTotal;
            }
        }

        return total;
    }
}