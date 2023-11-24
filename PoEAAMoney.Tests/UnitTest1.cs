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
        set { amount = Math.Round(value * GetCentsFactor[Currency], MidpointRounding.ToEven); }
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

    public static Money operator +(Money a, Money b)
    {
        AssertSameCurrency(a, b);
        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        AssertSameCurrency(a, b);
        return new Money(a.Amount - b.Amount, a.Currency);
    }

    public static bool operator >(Money a, Money b)
    {
        AssertSameCurrency(a, b);
        return a.Amount > b.Amount;
    }

    public static bool operator <(Money a, Money b)
    {
        AssertSameCurrency(a, b);
        return a.Amount < b.Amount;
    }

    public static Money operator *(Money a, decimal factor)
    {
        return new Money(a.Amount * factor, a.Currency);
    }

    private static void AssertSameCurrency(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new InvalidOperationException("Cannot add Money with different currencies");
        }
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
