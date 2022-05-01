namespace PersonalFinanceManager
{
    public class Bank
    {
        public static Dictionary<string, Bank> BankDictionary = new Dictionary<string, Bank>();

        private Dictionary<long, Account> accountDictionary = new Dictionary<long, Account>();

        public Dictionary<long, Account> AccountDictionary
        {
            get => accountDictionary;
            set => value = accountDictionary;
        }
        public IEnumerable<Account> Accounts => accountDictionary.Select(kvp => kvp.Value);

        private string name;
        public string Name { get => name; set => name = value; }
        private long routingNumber;
        public long RoutingNumber { get => routingNumber; set => routingNumber = value; }

        private static List<string> stringListOfAccounts;

        // private bool bankAcctDictHasUnsavedChanges = false;
        // private static bool bankDictHasUnsavedChanges = false;

        // ---------- METHODS ----------

        /// <summary> Returns a string value with one account per line containing all the accounts in this Bank's accountDictionary for the purpose of showing to the user </summary>
        public static List<string> GetAccountListFor(Bank thisBank)
        {
            stringListOfAccounts = new List<string>();
            foreach (KeyValuePair<long, Account> entry in thisBank.accountDictionary)
            {
                stringListOfAccounts.Add($"Account Holder: {entry.Value.HolderName} (XXXXX{(entry.Key.ToString()).Substring(4, 4)})  -  {entry.Value.Balance.ToString("c")} ");
            }

            return stringListOfAccounts;
        }


        public void AddAccount(Account thisAccount)
        {
            try
            {
                this.accountDictionary.Add(thisAccount.AccountNumber, thisAccount);
            }
            catch
            {
                throw new ValueNotAllowedException("That account number already exists in the system.");
            }
        }
        // public static void AddAccountToBank(Bank thisBank, string holderName, long accountNum)
        // {
        //     Account acct = new Account(holderName, accountNum);
        //     thisBank.accountDictionary.Add(accountNum, acct);
        //     // thisBank.bankAcctDictHasUnsavedChanges = true;
        // }

        public Account GetAccount(long accountNum)
        {
            return this.accountDictionary[accountNum];
            // Add some code to make it return an error if the account number is bad
        }
        public static Bank GetBank(string bankName)
        {
            return Bank.BankDictionary[bankName];
            // add code to constrain this
        }

        public string GetBankInfo()
        {
            return String.Format($"Bank Name: {Name} \n \n Routing Number: {RoutingNumber}");
        }

        public override string ToString()
        {
            return GetBankInfo();
        }

        public static void LoadAcctsFor(Bank thisBank)
        {
            if (File.Exists($"../Files/{thisBank.Name + "Accounts"}.json"))
            {
                var json = File.ReadAllText($"../Files/{thisBank.Name + "Accounts"}.json");
                thisBank.accountDictionary = System.Text.Json.JsonSerializer.Deserialize<Dictionary<long, Account>>(json);
            }
            else
            {
                // nothing to load
            }
        }

        // --------- CONSTRUCTORS ----------
        public Bank(string bankName, long routingNum)
        {
            if (bankName.Trim().Length <= 1 || bankName == null)
            {
                throw new ValueNotAllowedException("Bank name must not be a blank field and also must contain more than one character.");
            }
            else if (BankDictionary.ContainsKey(bankName))
            {
                throw new ValueNotAllowedException("A bank with that name already exists in the system.");
            }
            foreach (KeyValuePair<string, Bank> kvPair in Bank.BankDictionary)
            {
                if (kvPair.Value.RoutingNumber == routingNum)
                {
                    throw new ValueNotAllowedException("A bank with that routing number already exists in the system.");
                }
            }
            name = bankName.Trim();

            if (Convert.ToString(routingNum).Length != 9)
            {
                throw new ValueNotAllowedException("Routing number must be 9 digits, and the first digit must not be 0.");
            }

            routingNumber = routingNum;
        }

        public Bank()
        {
            // for loading with json
        }

    }
}
