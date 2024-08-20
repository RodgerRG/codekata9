using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Code.Kata._9.Data.Entities;

public class PricingRule
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PricingRuleId { get; init; }

    public float CalculateDiscount(float itemQuantity)
    {
        return 0;
    }
}