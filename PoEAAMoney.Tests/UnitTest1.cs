namespace PatternsOfEnterpriseApplicationArchitecture.BasePatterns.Tests;

public class MoneyTests_Arithmetics
{
    [Fact]
    public void TestAddsUp()
    {
        var tenEuros = Money.Euros(amount: 10);
        var twentyEuros = Money.Euros(amount: 20);
        var thirtyEuros = Money.Euros(amount: 30);
        Assert.Equal(thirtyEuros, tenEuros + twentyEuros);
    }

    [Fact]
    public void TestCannotAddDifferentCurrencies()
    {
        var tenEuros = Money.Euros(amount: 10);
        var tenDollars = Money.Dollars(amount: 10);
        Assert.Throws<InvalidOperationException>(() => tenEuros + tenDollars);
    }

    [Fact]
    public void TestSubtracts()
    {
        var thirtyEuros = Money.Euros(amount: 30);
        var tenEuros = Money.Euros(amount: 10);
        var twentyEuros = Money.Euros(amount: 20);
        Assert.Equal(twentyEuros, thirtyEuros - tenEuros);
    }

    [Fact]
    public void TestCannotSubtractDifferentCurrencies()
    {
        var tenEuros = Money.Euros(amount: 10);
        var tenDollars = Money.Dollars(amount: 10);
        Assert.Throws<InvalidOperationException>(() => tenEuros - tenDollars);
    }

    [Fact]
    public void TestMultiplication()
    {
        var tenEuros = Money.Euros(amount: 10);
        var twentyEuros = Money.Euros(amount: 20);
        Assert.Equal(twentyEuros, tenEuros * 2);
    }

    [Fact]
    public void TestRoundsToHalfEvenWhenMultiplying()
    {
        var oneEuro = Money.Euros(amount: 1);
        Assert.Equal(Money.Euros(amount: 2.76m), oneEuro * 2.756m);
        Assert.Equal(Money.Euros(amount: 2.75m), oneEuro * 2.754m);
    }
}

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
