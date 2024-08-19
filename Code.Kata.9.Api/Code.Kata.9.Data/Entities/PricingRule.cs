using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Code.Kata._9.Data.Entities;

public class PricingRule
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PricingRuleId { get; set; }
    
    public float CostPerUnit { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PricingUnit PricingUnit { get; set; }
    
    public float? QuantityThreshold { get; set; }
    
    public int? QuantityBuyDiscount { get; set; }
}