using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Kata._9.Data.Entities;

public class SalesItem
{
    public SalesItem(){}

    public SalesItem(string itemName, string itemDescription)
    {
        ItemName = itemName;
        ItemDescription = itemDescription;

        PricingInfos = new List<PricingInfo>();
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SalesItemId { get; set; }

    public string ItemName { get; set; }
    public string ItemDescription { get; set; }

    //Related Entities
    public ICollection<PricingInfo> PricingInfos { get; set; }
}