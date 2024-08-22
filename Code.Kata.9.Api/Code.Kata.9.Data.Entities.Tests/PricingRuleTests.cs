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
            "3 for 2", 3, 1
        );

        var originalPrice = (float)1.50;

        //ACT
        var cost = pricingRule.ComputeCost(PricingUnit.Each, originalPrice, 3);

        //ASSERT
        cost.Should().Be(3);
    }

    [Fact]
    public void GivenEachPricingRule_WithMoreQuantityThanSpecialPrice_ShouldCalculateDiscountCorrectly()
    {
        //ARRANGE
        var pricingRule = new PricingRule(
            "3 for 2", 3, 1
        );

        var originalPrice = (float)1.50;

        //ACT
        var cost = pricingRule.ComputeCost(PricingUnit.Each, originalPrice, 4);

        //ASSERT
        cost.Should().Be((float)4.50);
    }

    [Fact]
    public void GivenEachPricingRule_WithNoQuantity_ShouldCalculateDiscountCorrectly()
    {
        //ARRANGE
        var pricingRule = new PricingRule(
            "3 for 2",3, 1
        );

        var originalPrice = (float)1.50;

        //ACT
        var cost = pricingRule.ComputeCost( PricingUnit.Each, originalPrice, 0);

        //ASSERT
        cost.Should().Be(0);
    }

    [Fact]
    public void GivenEachPricingRule_WithLessQuantityThanSpecialPrice_ShouldCalculateDiscountCorrectly()
    {
        //ARRANGE
        var pricingRule = new PricingRule(
            "3 for 2", 3, 1
        );

        var originalPrice = (float)1.50;

        //ACT
        var cost = pricingRule.ComputeCost(PricingUnit.Each,originalPrice, 2);

        //ASSERT
        cost.Should().Be(3);
    }

    [Fact]
    public void GivenWeightPricingRule_WithLessQuantityThanSpecialPrice_ShouldCalculateDiscountCorrectly()
    {
        //ARRANGE
        var pricingRule = new PricingRule(
            "1.50 per KG on purchases over 3KG", 3, (float)1.50
        );

        var originalPrice = (float)3.50;

        //ACT
        var cost = pricingRule.ComputeCost(PricingUnit.UnitWeight, originalPrice, 2);

        //ASSERT
        cost.Should().Be(7);
    }

    [Fact]
    public void GivenWeightPricingRule_WithMoreQuantityThanSpecialPrice_ShouldCalculateDiscountCorrectly()
    {
        //ARRANGE
        var pricingRule = new PricingRule(
            "1.50 per KG on purchases over 3KG", 3, (float)1.50
        );

        var originalPrice = (float)3.50;

        //ACT
        var cost = pricingRule.ComputeCost(PricingUnit.UnitWeight,originalPrice, 4);

        //ASSERT
        cost.Should().Be(6);
    }
}