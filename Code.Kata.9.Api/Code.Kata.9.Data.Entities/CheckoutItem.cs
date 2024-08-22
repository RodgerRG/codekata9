using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Kata._9.Data.Entities;

public class CheckoutItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CheckoutItemId { get; set; }
    public int CheckoutId { get; set; }
    public int SalesItemId { get; set; }
    public DateTime ItemScannedTime { get; set; }
    public float ItemRowQuantity { get; set; }
    
    //Related Entities
    public SalesItem SalesItem { get; set; }
    
}