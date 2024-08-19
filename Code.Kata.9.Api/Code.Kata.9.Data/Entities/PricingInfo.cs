using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Kata._9.Data.Entities;

public class PricingInfo
{
    private ICollection<PricingRule> _activeRules = new List<PricingRule>();
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PricingInfoId { get; set; }
    public int SalesItemId { get; set; }
    public int PricingCatalogueId { get; set; }
    
    //Related Entities
    public SalesItem SalesItem { get; set; }
    public PricingCatalogue PricingCatalogue { get; set; }
    public ICollection<PricingRule> PricingRules { get; set; }
}