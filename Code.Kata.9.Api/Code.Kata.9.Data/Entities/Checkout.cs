using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Kata._9.Data.Entities;

public class Checkout
{
    public Checkout(ICollection<CheckoutItem> items)
    {
        CheckoutItems = items;
        InitialiseCheckout();
    }
    
    private readonly Dictionary<int, float> _checkoutLookup = new (); 
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CheckoutId { get; set; }
    
    public ICollection<CheckoutItem> CheckoutItems { get; init; }
    public DateTime ExpiryDate { get; set; }
    
    public void Scan(CheckoutItem item)
    {
        CheckoutItems.Add(item);

        if (_checkoutLookup.Remove(item.SalesItemId, out var existingQuantity))
        {
            _checkoutLookup.Add(item.SalesItemId, existingQuantity + item.Quantity);
            return;
        }
        
        _checkoutLookup.Add(item.SalesItemId, item.Quantity);
    }

    public void InitialiseCheckout()
    {
        if (_checkoutLookup.Values.Count > 0)
            throw new InvalidOperationException("Cannot initialise a checkout that has already been initialised");
        
        foreach(var item in CheckoutItems) Scan(item);
    }
}