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

        // ---------- METHODS ------------

        public void CreateAndAddCustomCategory(string customCategoryName)
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

        ///<summary>This method is used for adding an existing custom category from a file.</summary>
        public void AddCustomCategory(CustomCategory newCustomCategoryToAdd)
        {
            this.customCategoryDictionary.Add(newCustomCategoryToAdd.CategoryName, newCustomCategoryToAdd);
        }

        public static void SaveCustomCategoriesFor(SubAccount thisSubAccount)
        {
            // Note to self: make bool check for unsaved changes(?)

            // if (thisAccount.SubAcctListHasUnsavedChanges)

            if (!(File.Exists($"../Files/{"CustomCategoriesFor" + thisSubAccount.ItemKey}.txt")))
            {
                File.Create($"../Files/{"CustomCategoriesFor" + thisSubAccount.ItemKey}.txt");
            }
            else
            {
                //Clear contents of file:
                using (FileStream fs = File.Open($"../Files/{"CustomCategoriesFor" + thisSubAccount.ItemKey}.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    lock (fs)
                    {
                        fs.SetLength(0);
                    }
                }
            }

            StreamWriter fileWriter = new StreamWriter($"../Files/{"CustomCategoriesFor" + thisSubAccount.ItemKey}.txt");

            foreach (KeyValuePair<string, CustomCategory> keyValuePair in thisSubAccount.customCategoryDictionary)
            {
                fileWriter.WriteLine("Balance:" + keyValuePair.Value.Balance);
                fileWriter.WriteLine("CustomCategory Name:" + keyValuePair.Value.CategoryName);
                fileWriter.WriteLine("End:Yes");
            }
            fileWriter.Flush();
            fileWriter.Close();

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

        public static Dictionary<string, CustomCategory> LoadCustomCategoriesFor(SubAccount thisSubAccount)
        {
            if (File.Exists($"../Files/{"CustomCategoriesFor" + thisSubAccount.ItemKey}.txt"))
            {
                var customCategories = new Dictionary<string, CustomCategory>();
                string categoryName = "";
                decimal balance = 0M;

                foreach (var line in File.ReadAllLines($"../Files/{"CustomCategoriesFor" + thisSubAccount.ItemKey}.txt"))
                {
                    var parts = line.Split(':');
                    if (parts[0] == "Balance")
                    {
                        balance = decimal.Parse(parts[1]);
                    }

                    else if (parts[0] == "CustomCategory Name")
                    {
                        categoryName = parts[2];
                    }

                    else if (parts[0] == "End")
                    {
                        thisSubAccount.AddCustomCategory(new CustomCategory(balance, categoryName));
                    }
                }

                return customCategories;
            }
            else
            {
                return new Dictionary<string, CustomCategory>();
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
