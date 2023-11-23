namespace PoEAAMoney.Tests;

public enum Currency
{
    BRL,
    USD,
    EUR
}

class Money
{
    public readonly Currency Currency;
    private decimal amount;
    public decimal Amount
    {
        get { return amount / GetCentsFactor[Currency]; }
        set { amount = value * GetCentsFactor[Currency]; }
    }

    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static readonly Dictionary<Currency, decimal> GetCentsFactor = new()
    {
        { Currency.BRL, 100 },
        { Currency.USD, 100 },
        { Currency.EUR, 100 }
    };

    public static Money Dollars(decimal amount) => new(amount, Currency.USD);

    public static Money BrasilianReal(decimal amount) => new(amount, Currency.BRL);

    public static Money Euros(decimal amount) => new(amount, Currency.EUR);

    public override int GetHashCode() => HashCode.Combine(Amount, Currency);

    public override bool Equals(object? obj)
    {
        if (obj is not Money other) return false;
        return Amount == other.Amount && Currency == other.Currency;
    }
}

public class MoneyTests
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
