/* A console application to demonstrate how the underlying data entities
 * can be used to display how the solution to Kata #9 works.
 */

using Code.Kata._9.Data.Entities;

var randomiser = new Random();

var pricingCatalogue = new PricingCatalogue(DateTime.UtcNow, TimeSpan.FromDays(7));
pricingCatalogue.PricingCatalogueId = 1;


// Set up the sales items
// Set up the pricing catalogue with pricing info
for (int i = 1; i <= 100; i++)
{
    var salesItem = new SalesItem(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
    salesItem.SalesItemId = i;
    var pricingInfo =
        new PricingInfo(salesItem.SalesItemId, 1, (float)randomiser.NextDouble() * 5, Guid.NewGuid().ToString());
    salesItem.PricingInfos.Add(pricingInfo);
    pricingInfo.SalesItem = salesItem;

    if (randomiser.Next(0, 20) > 14)
    {
        var pricingRule = new PricingRule(Guid.NewGuid().ToString(), (PricingUnit)randomiser.Next(0, 2),
            (float)randomiser.NextDouble() * 5, (float)randomiser.NextDouble() * 3);
        pricingRule.PricingInfo = pricingInfo;
        pricingRule.PricingInfoId = pricingInfo.PricingInfoId;
        pricingInfo.AlternatePricing.Add(pricingRule);
    }

    pricingCatalogue.PricingInfos.Add(pricingInfo);
}

Console.WriteLine("Welcome to the random shop - please buy some items");
Console.WriteLine("===================================");
Console.WriteLine();
Console.WriteLine(
    "Enter an item number between 1 - 100 and an item quantity, separated by a comma to purchase an item. Or press backspace to checkout your cart");

var isStillShopping = true;
var shoppingItems = new List<char>();

var checkout = new Checkout(new List<CheckoutItem>());
checkout.PricingCatalogue = pricingCatalogue;
checkout.PricingCatalogueId = pricingCatalogue.PricingCatalogueId;

while (isStillShopping)
{
    var input = Console.ReadKey();
    if (input.Key == ConsoleKey.Backspace)
    {
        isStillShopping = false;
        continue;
    }

    if (input.Key == ConsoleKey.Enter)
    {
        var shoppingList = new string(shoppingItems.ToArray());
        shoppingItems = new List<char>();
        Console.WriteLine();

        var itemInfo = shoppingList.Split(",");
        if (Int32.TryParse(itemInfo[0].Trim(), out var itemNumber) &&
            float.TryParse(itemInfo[1].Trim(), out var quantity))
        {
            var checkoutItem = new CheckoutItem();
            checkoutItem.SalesItemId = itemNumber;
            checkoutItem.ItemRowQuantity = quantity;

            checkout.Scan(checkoutItem);
        }

        continue;
    }

    shoppingItems.Add(input.KeyChar);
}

Console.WriteLine($"Thank you for your purchases! The total comes out to: {checkout.Total}");
Console.WriteLine("=======================");
Console.WriteLine("Here is a breakdown of your purchases:");
Console.WriteLine("=======================");
Console.WriteLine();

var itemTotals = checkout.GetItemTotals();
foreach (var (key, value) in itemTotals)
{
    var pricingInfo = pricingCatalogue.PricingInfos.FirstOrDefault(pi => pi.SalesItemId == key);
    if (pricingInfo is null)
        throw new InvalidOperationException(
            "Sales item in checkout has no pricing info in catalogue but was purchased!");
    
    Console.WriteLine($"You bought {value} {pricingInfo.SalesItem.ItemName} for a total cost of ${pricingInfo.ComputeCost(value)}");
}