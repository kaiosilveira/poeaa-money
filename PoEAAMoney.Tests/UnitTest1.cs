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
}
