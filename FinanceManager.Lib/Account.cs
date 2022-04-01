namespace PersonalFinanceManager
{
    public class Account : IAccount
    {
        internal decimal balance;

        private int accountNumber;
        private string holderName;
        public string HolderName{get; private set;}

        public virtual decimal Balance
        {
            get => balance;
            private set
            {
                if(value<0)
                {
                    balance += value;
                }

                // Make this more secure?
            }
        }
    }
}
