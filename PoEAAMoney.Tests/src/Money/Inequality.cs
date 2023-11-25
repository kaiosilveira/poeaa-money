namespace PatternsOfEnterpriseApplicationArchitecture.BasePatterns.Tests;

public class MoneyTests_Inequality
{
    [Fact]
    public void TestGreaterThanComparisons()
    {
        var tenEuros = Money.Euros(amount: 10);
        var elevenEuros = Money.Euros(amount: 11);

        Assert.True(elevenEuros > tenEuros);
        Assert.True(tenEuros < elevenEuros);
    }

    [Fact]
    public void TestCannotCompareDifferentCurrencies()
    {
        var tenEuros = Money.Euros(amount: 10);
        var tenDollars = Money.Dollars(amount: 10);
        Assert.Throws<InvalidOperationException>(() => tenEuros > tenDollars);
    }
}
