public class SubAccount
{
    public AccountTypes AccountType { get; }

    public enum AccountTypes
    {
        Checking,
        Savings,
        MoneyMarket
    }

}