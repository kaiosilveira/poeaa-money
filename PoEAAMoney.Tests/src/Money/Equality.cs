namespace PatternsOfEnterpriseApplicationArchitecture.BasePatterns.Tests;

public class MoneyTests_Equality
{
    [Fact]
    public void TestIsEqualSameAmountSameCurrency()
    {
        var tenEuros1 = Money.Euros(amount: 10);
        var tenEuros2 = Money.Euros(amount: 10);
        Assert.Equal(tenEuros1, tenEuros2);
    }

    [Fact]
    public void TestIsNotEqualDifferentAmountSameCurrency()
    {
        var tenEuros = Money.Euros(amount: 10);
        var twentyEuros = Money.Euros(amount: 20);
        Assert.NotEqual(tenEuros, twentyEuros);
    }

    [Fact]
    public void TestIsNotEqualSameAmountDifferentCurrency()
    {
        var tenEuros = Money.Euros(amount: 10);
        var tenDollars = Money.Dollars(amount: 10);
        Assert.NotEqual(tenEuros, tenDollars);
    }
}
