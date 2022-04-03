namespace PersonalFinanceManager
{
    /// <summary>
    /// "SubAccount" class contains the following variables: 
    /// decimal balance; int accountNumber; string holderName
    /// </summary>
    public class SubAccount : ICategorizableAccount, IAccount
    {
        public AccountTypes AccountType { get; }
        private decimal balance;
        public decimal Balance
        {
            get => balance;

            private set
            {
                if (balance + value < 0)
                {
                    throw new ValueNotAllowedException("ERROR: Action failed. This action would leave a deficit in your account.");
                }
                balance = value;
            }
        }
        private int accountNumber;
        public int AccountNumber => accountNumber;
        public string ItemKey => AccountType.ToString();
        public Account BaseAccount;
        private int numberOfSubAccounts = 0;

        private Dictionary<string, CustomCategory> customCategoryDictionary = new Dictionary<string, CustomCategory>();

        public enum AccountTypes
        {
            Checking,
            Savings,
            MoneyMarket
        }


        // ---------- VOID METHODS ------------
        public static void AddCustomCategory(SubAccount thisSubAccount, CustomCategory customCategoryToAdd)
        {
            thisSubAccount.customCategoryDictionary.Add(customCategoryToAdd.ItemKey, customCategoryToAdd);
        }

        // ----------- CONSTRUCTORS ------------

        public SubAccount(AccountTypes type, Account baseAccount)
        {
            if (numberOfSubAccounts == 99)
            {
                throw new MaximumReachedException("ERROR: You have reached the maximum number of sub-accounts for this account.");
            }
            AccountType = type;
            BaseAccount = baseAccount;
            numberOfSubAccounts++;
            accountNumber = baseAccount.AccountNumber * 10 + numberOfSubAccounts;
        }

    }
}