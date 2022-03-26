namespace PersonalFinanceManager
{
    public class SubAccount : Account, IAccount
    {
        public AccountTypes AccountType { get; }
        public enum AccountTypes
        {
            Checking,
            Savings,
            MoneyMarket
        }

    }
}