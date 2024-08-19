using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Kata._9.Data.Entities;

public abstract class SalesItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SalesItemId { get; set; }
    
    //Related Entities
    public ICollection<PricingInfo> PricingInfos { get; set; }
}