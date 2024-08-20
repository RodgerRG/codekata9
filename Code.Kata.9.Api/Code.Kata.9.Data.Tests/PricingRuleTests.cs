using Code.Kata._9.Data.Entities;
using FluentAssertions;

namespace Code.Kata._9.Data.Tests;

public class PricingRuleTests
{
    [Fact]
    public void GivenAPricingRule_WithUnitOfEach_ShouldCalculateDiscountCorrectly()
    {
        //ARRANGE
        var pricingRule = new PricingRule(
            "3 for 2", PricingUnit.Each, 3, 1
            );

        var originalPrice = (float) 1.50;

        //ACT
        var cost = pricingRule.ComputeCost(originalPrice, 3);

        //ASSERT
        cost.Should().Be(3);
    }

    [Fact]
    public void GivenEachPricingRule_WithMoreQuantityThanSpecialPrice_ShouldCalculateDiscountCorrectly()
    {
        //ARRANGE
        var pricingRule = new PricingRule(
            "3 for 2", PricingUnit.Each, 3, 1
        );

        var originalPrice = (float) 1.50;

        //ACT
        var cost = pricingRule.ComputeCost(originalPrice, 4);

        //ASSERT
        cost.Should().Be((float) 4.50);
    }
    
    [Fact]
    public void GivenEachPricingRule_WithNoQuantity_ShouldCalculateDiscountCorrectly()
    {
        //ARRANGE
        var pricingRule = new PricingRule(
            "3 for 2", PricingUnit.Each, 3, 1
        );

        var originalPrice = (float) 1.50;

        //ACT
        var cost = pricingRule.ComputeCost(originalPrice, 0);

        //ASSERT
        cost.Should().Be(0);
    }
    
    [Fact]
    public void GivenEachPricingRule_WithLessQuantityThanSpecialPrice_ShouldCalculateDiscountCorrectly()
    {
        //ARRANGE
        var pricingRule = new PricingRule(
            "3 for 2", PricingUnit.Each, 3, 1
        );

        var originalPrice = (float) 1.50;

        //ACT
        var cost = pricingRule.ComputeCost(originalPrice, 2);

        //ASSERT
        cost.Should().Be(3);
    }
}