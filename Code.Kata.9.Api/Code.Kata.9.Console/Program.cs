/* A console application to demonstrate how the underlying data entities
 * can be used to display how the solution to Kata #9 works.
 */

using Code.Kata._9.Data.Entities;

var pricingCatalogue = new PricingCatalogue(DateTime.UtcNow, TimeSpan.FromDays(7));
