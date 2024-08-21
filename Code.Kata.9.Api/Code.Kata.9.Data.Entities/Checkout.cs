using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Kata._9.Data.Entities;

public class Checkout
{
    public Checkout(ICollection<CheckoutItem> items)
    {
        CheckoutItems = new List<CheckoutItem>();
        Total = 0;
        InitialiseCheckout(items);
    }
    
    private readonly Dictionary<int, float> _checkoutLookup = new (); 
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CheckoutId { get; set; }
    public int PricingCatalogueId { get; set; }
    public ICollection<CheckoutItem> CheckoutItems { get; set; }
    public float Total { get; set; }
    public DateTime ExpiryDate { get; set; }
    public PricingCatalogue PricingCatalogue { get; set; }
    
    public void Scan(CheckoutItem item)
    {
        CheckoutItems.Add(item);

        if (_checkoutLookup.Remove(item.SalesItemId, out var existingQuantity))
        {
            var updatedQuantity = existingQuantity + item.ItemRowQuantity;
            _checkoutLookup.Add(item.SalesItemId, updatedQuantity);
            ComputeTotal(item, existingQuantity, updatedQuantity);
            return;
        }
        
        _checkoutLookup.Add(item.SalesItemId, item.ItemRowQuantity);
        ComputeTotal(item, 0, item.ItemRowQuantity);
    }

    private void ComputeTotal(CheckoutItem item, float oldQuantity, float newQuantity)
    {
        Total -= item.ComputeTotal(oldQuantity, PricingCatalogueId);
        Total += item.ComputeTotal(newQuantity, PricingCatalogueId);
    }

    public void InitialiseCheckout(ICollection<CheckoutItem> items)
    {
        if (_checkoutLookup.Values.Count > 0)
            throw new InvalidOperationException("Cannot initialise a checkout that has already been initialised");
        
        foreach(var item in items) Scan(item);
    }
}