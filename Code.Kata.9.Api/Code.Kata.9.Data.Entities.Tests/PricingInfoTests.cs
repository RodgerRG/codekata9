using Code.Kata._9.Data.Entities;
using FluentAssertions;

namespace Code.Kata._9.Data.Tests;

public class PricingInfoTests
{
    [Fact]
    public void GivenNoSpecialPricingRules_WhenComputingCost_ThenCorrectTotalIsReturned()
    {
        //ARRANGE
        var pricingInfo = new PricingInfo(
            1, 1, 2, "Per Dozen"
            );

        //ACT
        var cost = pricingInfo.ComputeCost(3);

        //ASSERT
        cost.Should().Be(6);
    }
    
    [Fact]
    public void GivenASpecialPricingRuleGreaterThanOriginal_WhenComputingCost_ThenCorrectTotalIsReturned()
    {
        //ARRANGE
        var pricingInfo = new PricingInfo(
            1, 1, 2, "Per Dozen"
        );
        
        pricingInfo.AlternatePricing.Add(
            new PricingRule("More Expensive Somehow", PricingUnit.Each, 1, 3)
                );

        //ACT
        var cost = pricingInfo.ComputeCost(3);

        //ASSERT
        cost.Should().Be(6);
    }

    [Fact]
    public void GivenASpecialPricingRuleLowerThanOriginal_WhenComputingCost_ThenCorrectTotalIsReturned()
    {
        //ARRANGE
        var pricingInfo = new PricingInfo(
            1, 1, 2, "Per Dozen"
        );
        
        pricingInfo.AlternatePricing.Add(
            new PricingRule("Cheaper", PricingUnit.Each, 2, (float) 0.5)
        );

        //ACT
        var cost = pricingInfo.ComputeCost(3);

        //ASSERT
        cost.Should().Be(3);
    }

    [Fact]
    public void GivenTwoPricingRules_WhenComputingCost_ThenCheapestOneIsApplied()
    {
        //ARRANGE
        var pricingInfo = new PricingInfo(
            1, 1, 2, "Per Dozen"
        );
        
        pricingInfo.AlternatePricing.Add(
            new PricingRule("Cheaper", PricingUnit.Each, 2, (float) 0.8)
        );
        
        pricingInfo.AlternatePricing.Add(
            new PricingRule("Cheaper", PricingUnit.Each, 2, (float) 0.5)
        );

        //ACT
        var cost = pricingInfo.ComputeCost(3);

        //ASSERT
        cost.Should().Be(3);
    }
}