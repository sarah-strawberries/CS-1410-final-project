namespace PersonalFinanceManager
{
    public abstract class Account
    {
        private decimal balance;

        public virtual decimal Balance
        {
            get => balance;
            set
            {
                if (balance + value < 0)
                {
                    throw new ValueNotAllowedException("This action would leave a deficit in your account. Action not allowed.");
                }
                balance = value;
            }
        }
        public enum AccountType
        {
            Checking,
            Savings,
            MoneyMarket
        }

        public virtual decimal GetBalance()
        {
            return Balance;
        }
    }
}