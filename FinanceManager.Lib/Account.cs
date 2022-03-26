namespace PersonalFinanceManager
{
    public class Account : IAccount
    {
        private decimal balance;

        private int accountNumber;
        private string holderName;
        public string HolderName{get; private set;}

        public virtual decimal Balance
        {
            get => balance;
            private set
            {
                // if (balance + value < 0)
                // {
                //     throw new ValueNotAllowedException("This action would leave a deficit in your account. Action not allowed.");
                // }
                // balance = value;
                // ^ may not need the above code here because it might fit better in the SubAccount class
            }
        }
    }
}
