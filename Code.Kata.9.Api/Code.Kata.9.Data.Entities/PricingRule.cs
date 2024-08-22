using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Code.Kata._9.Data.Entities;

public class PricingRule
{
    //Default Constructor for EF core
    public PricingRule()
    {
    }

    public PricingRule(string pricingRuleName, float discountQuantityThreshold,
        float costPerUnit)
    {
        PricingRuleName = pricingRuleName;
        DiscountQuantityThreshold = discountQuantityThreshold;
        CostPerUnit = costPerUnit;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PricingRuleId { get; init; }
    public int PricingInfoId { get; set; }
    public string PricingRuleName { get; init; }
    public float DiscountQuantityThreshold { get; init; }
    public float CostPerUnit { get; init; }
    
    //Related Entities
    public PricingInfo PricingInfo { get; set; }

    public float ComputeCost(PricingUnit pricingUnit, float originalCostPerUnit, float itemQuantity)
    {
        var total = pricingUnit switch
        {
            PricingUnit.Each => CalculateEachTotal(originalCostPerUnit, itemQuantity),
            PricingUnit.UnitWeight => CalculateWeightTotal(originalCostPerUnit, itemQuantity),
            _ => throw new ArgumentOutOfRangeException()
        };
        return total;
    }

    private float CalculateEachTotal(float originalCostPerUnit, float itemQuantity)
    {
        var integerItemQuantity = (int)Math.Round(itemQuantity);
        var integerItemThreshold = (int)Math.Round(DiscountQuantityThreshold);
        //items not covered by the discount
        var remaining = integerItemQuantity % integerItemThreshold;
        var total = (integerItemQuantity - remaining) * CostPerUnit + remaining * originalCostPerUnit;
        return total;
    }

    private float CalculateWeightTotal(float originalCostPerUnit, float itemQuantity)
    {
        if (itemQuantity > DiscountQuantityThreshold) return CostPerUnit * itemQuantity;
        return originalCostPerUnit * itemQuantity;
    }
}