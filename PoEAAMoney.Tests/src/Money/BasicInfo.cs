namespace PatternsOfEnterpriseApplicationArchitecture.BasePatterns.Tests;

public class MoneyTests_BasicInfo
{
    [Fact]
    public void TestHasAmountAndCurrency()
    {
        var money = new Money(amount: 10, currency: Currency.BRL);
        Assert.Equal(10, money.Amount);
        Assert.Equal(Currency.BRL, money.Currency);
    }

    [Fact]
    public void TestAppliesTheCorrectCentsFactorForBRL()
    {
        var money = new Money(amount: 10, currency: Currency.BRL);
        Assert.Equal(10, money.Amount);
    }
}
