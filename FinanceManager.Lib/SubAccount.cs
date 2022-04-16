namespace PersonalFinanceManager
{
    public class SubAccount : ICategorizableAccount, IAccount
    {
        public SubAccountTypes AccountType { get; }
        private decimal balance;
        public decimal Balance
        {
            get => balance;

            private set
            {
                if (balance + value < 0)
                {
                    throw new ValueNotAllowedException("Action failed. This action would leave a deficit in your account.");
                }
                balance = value;
            }
        }
        private long accountNumber;
        public long AccountNumber => accountNumber;
        public string ItemKey => AccountType.ToString();
        public Account BaseAccount;

        private Dictionary<string, CustomCategory> customCategoryDictionary = new Dictionary<string, CustomCategory>();

        public enum SubAccountTypes
        {
            Checking,
            Savings,
            MoneyMarket
        }


        // ---------- VOID METHODS ------------
        public void AddCustomCategory(string customCategoryName)
        {
            if (customCategoryName.Trim().Length < 1)
            {
                throw new ValueNotAllowedException("Custom category name must be at least one character excluding spaces.");
            }
            else
            {
                CustomCategory category = new CustomCategory(customCategoryName);
                this.customCategoryDictionary.Add(customCategoryName, category);
            }
        }

        public CustomCategory GetCustomCategory(string key)
        {
            if (this.customCategoryDictionary.ContainsKey(key))
            {
                return this.customCategoryDictionary[key];
            }
            else
            {
                throw new ArgumentException("A custom category with that key does not exist.");
            }
        }

        // ----------- CONSTRUCTORS ------------

        /// <summary> **This constructor for testing only** </summary>
        public SubAccount(SubAccountTypes type)
        {
            AccountType = type;
            accountNumber = 87654321;
        }

        public SubAccount(SubAccountTypes type, Account baseAccount)
        {
            // if (baseAccount.NumberOfSubAccounts == 100)
            // {
            //     throw new MaximumReachedException("ERROR: You have reached the maximum number of sub-accounts for this account.");
            // }  ^-- this code is probably not needed because due to the enum of account types being the key in the dictionary, 
            //          the number of accounts should never pass the number of items in the enum
            AccountType = type;
            BaseAccount = baseAccount;
            accountNumber = baseAccount.AccountNumber * 100 + baseAccount.NumberOfSubAccounts + 1;
        }
        /// <summary>This constructor is for loading saved SubAccounts only.</summary>
        public SubAccount(SubAccountTypes type, decimal acctBalance, long subAccountNumber, Account baseAccount)
        {
            AccountType = type;
            balance = acctBalance;
            accountNumber = subAccountNumber;
            BaseAccount = baseAccount;
        }

    }
}