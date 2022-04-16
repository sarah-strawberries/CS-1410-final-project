namespace PersonalFinanceManager
{
    public class Bank : IStorageService
    {
        public static Dictionary<string, Bank> bankDictionary = new Dictionary<string, Bank>();

        private Dictionary<long, Account> accountDictionary = new Dictionary<long, Account>();
        private string name;
        private int routingNumber;
        // private bool bankAcctDictHasUnsavedChanges = false;
        // private static bool bankDictHasUnsavedChanges = false;

        private static List<string> stringListOfAccounts;
        public string Name { get => name; }
        public int RoutingNumber { get => routingNumber; }


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
            return Bank.bankDictionary[bankName];
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

        public static void SaveAccountsFor(Bank thisBank)
        {
            // Note to self:Need to make bool check for unsaved changes
            // if (thisBank.bankAcctDictHasUnsavedChanges)

            if (!(File.Exists($@"C:\Users\Allen\code\CS-1410-final-project\Files\{thisBank.Name + "Accounts"}.txt")))
            {
                File.Create($@"C:\Users\Allen\code\CS-1410-final-project\Files\{thisBank.Name + "Accounts"}.txt");
            }

            StreamWriter fileWriter = new StreamWriter($@"C:\Users\Allen\code\CS-1410-final-project\Files\{thisBank.Name + "Accoounts"}.txt");

            foreach (KeyValuePair<long, Account> keyValuePair in thisBank.accountDictionary)
            {
                fileWriter.WriteLine("Account Number:" + keyValuePair.Value.AccountNumber);
                fileWriter.WriteLine("Balance:" + keyValuePair.Value.Balance);
                fileWriter.WriteLine("");
            }
            fileWriter.Flush();
            fileWriter.Close();
            // thisBank.bankAcctDictHasUnsavedChanges = false;


        }

        public static void SaveBanks()
        {
            // Note to self:Need to make bool check for unsaved changes

            // if (bankDictHasUnsavedChanges)
            // {

            StreamWriter fileWriter = new StreamWriter(@"C:\Users\Allen\code\CS-1410-final-project\Files\Banks.txt");

            foreach (KeyValuePair<string, Bank> keyValuePair in Bank.bankDictionary)
            {
                fileWriter.WriteLine("BankName:" + keyValuePair.Key);
                fileWriter.WriteLine("BankRoutingNum:" + keyValuePair.Value.RoutingNumber);
            }
            fileWriter.Flush();
            fileWriter.Close();
            // bankDictHasUnsavedChanges = false;

            // }
        }

        //---------------DATA STORAGE--------------

        public void StoreData()
        {
            SaveAccountsFor(this);
            Bank.SaveBanks();
        }

        public void ChangeStoredData()
        {
            throw new NotImplementedException();
        }

        public void LoadData()
        {
        // Load Banks and accounts

        }

        public void DeleteData()
        {
            throw new NotImplementedException();
        }



        // --------- CONSTRUCTORS ----------
        public Bank(string bankName, int routingNum)
        {
            if (bankName.Trim().Length <= 1 || bankName == null)
            {
                throw new ValueNotAllowedException("Bank name must not be a blank field and also must contain more than one character.");
            }
            else if (bankDictionary.ContainsKey(bankName))
            {
                throw new ValueNotAllowedException("A bank with that name already exists in the system.");
            }
            foreach (KeyValuePair<string, Bank> kvPair in Bank.bankDictionary)
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
            bankDictionary.Add(bankName, this);
            // bankDictHasUnsavedChanges = true;

        }

    }
}