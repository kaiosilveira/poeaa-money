namespace PoEAAMoney.Tests;

public enum Currency
{
    BRL,
    USD,
    EUR
}

class Money(decimal amount, Currency currency)
{
    public readonly decimal Amount = amount;
    public readonly Currency Currency = currency;
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
}
