namespace PatternsOfEnterpriseApplicationArchitecture.BasePatterns.Tests;

public class MoneyTests_Shortcuts
{
    [Fact]
    public void TestCreatesAnUSD()
    {
        var money = Money.Dollars(amount: 10);
        Assert.Equal(Currency.USD, money.Currency);
    }

    [Fact]
    public void TestCreatesBRL()
    {
        var money = Money.BrasilianReal(amount: 10);
        Assert.Equal(Currency.BRL, money.Currency);
    }

    [Fact]
    public void TestCreatesEUR()
    {
        var money = Money.Euros(amount: 10);
        Assert.Equal(Currency.EUR, money.Currency);
    }
}

public class MoneyTests_Allocation
{
    [Fact]
    public void TestAllocation()
    {
        var tenEuros = Money.Euros(amount: 10);
        var allocated = tenEuros.Allocate(ratios: [1, 1, 1]);
        Assert.Equal(Money.Euros(amount: 3.34m), allocated[0]);
        Assert.Equal(Money.Euros(amount: 3.33m), allocated[1]);
        Assert.Equal(Money.Euros(amount: 3.33m), allocated[2]);
    }

    [Fact]
    public void TestSolvesFoemmelsConundrum()
    {
        var amount = Money.Euros(amount: 0.05m);
        var allocated = amount.Allocate(ratios: [3, 7]);
        Assert.Equal(Money.Euros(amount: 0.02m), allocated[0]);
        Assert.Equal(Money.Euros(amount: 0.03m), allocated[1]);
    }
}
