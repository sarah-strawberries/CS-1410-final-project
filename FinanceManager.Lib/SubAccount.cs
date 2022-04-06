namespace PersonalFinanceManager
{
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
        private long accountNumber;
        public long AccountNumber => accountNumber;
        public string ItemKey => AccountType.ToString();
        public Account BaseAccount;

        private Dictionary<string, CustomCategory> customCategoryDictionary = new Dictionary<string, CustomCategory>();

        public enum AccountTypes
        {
            Checking,
            Savings,
            MoneyMarket
        }


        // ---------- VOID METHODS ------------
        public static void AddCustomCategory(SubAccount thisSubAcct, string customCategoryName)
        {
            CustomCategory category = new CustomCategory(customCategoryName);
            thisSubAcct.customCategoryDictionary.Add(customCategoryName, category);
        }

        public static CustomCategory GetCustomCategory(SubAccount thisSubAccount, string key)
        {
            return thisSubAccount.customCategoryDictionary[key];

            // Add some code to make it return an error if it has a bad key
        }

        // ----------- CONSTRUCTORS ------------

        /// <summary> **This constructor for testing only** </summary>
        public SubAccount(AccountTypes type)
        {
            AccountType = type;
            accountNumber = 87654321;
        }

        public SubAccount(AccountTypes type, Account baseAccount)
        {
            if (baseAccount.NumberOfSubAccounts == 100)
            {
                throw new MaximumReachedException("ERROR: You have reached the maximum number of sub-accounts for this account.");
            }
            AccountType = type;
            BaseAccount = baseAccount;
            accountNumber = baseAccount.AccountNumber * 100 + baseAccount.NumberOfSubAccounts;
        }

    }
}