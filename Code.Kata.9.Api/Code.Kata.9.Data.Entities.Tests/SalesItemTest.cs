using Code.Kata._9.Data.Entities;
using FluentAssertions;

namespace Code.Kata._9.Data.Tests;

public class SalesItemTest
{
    [Fact]
    public void GivenMultiplePricingInfo_WhenComputingCost_ThenCorrectPricingIsApplied()
    {
        //ARRANGE
        var salesItem = new SalesItem("Item One", "The first item");
        salesItem.PricingInfos.Add(
            new PricingInfo(1, 1, 2, "each"));
        
        salesItem.PricingInfos.Add(new PricingInfo(1, 2, 3, "each"));

        //ACT
        var cost = salesItem.ComputeCost(2, 2);

        //ASSERT
        cost.Should().Be(6);
    }
}