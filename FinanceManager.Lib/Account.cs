namespace PersonalFinanceManager
{
    public class Account
    {
        private decimal balance;

        private int accountNumber;

        public virtual decimal Balance
        {
            get => balance;
            set
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
