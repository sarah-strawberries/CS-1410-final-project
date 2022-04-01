namespace PersonalFinanceManager
{
    public class SubAccount : Account, IAccount
    {
        public AccountTypes AccountType { get; }

        public override decimal Balance 
        {
            get => base.Balance;
            
            private set
            {
                if (base.balance + value < 0)
                {
                    throw new ValueNotAllowedException("This action would leave a deficit in your account. Action not allowed.");
                }
                balance = value;
            }

        }
        public enum AccountTypes
        {
            Checking,
            Savings,
            MoneyMarket
        }

    }
}