namespace PersonalFinanceManager
{
    public class Bank : IStorageService
    {
        public static Dictionary<string, Bank> bankDictionary = new Dictionary<string, Bank>();

        private Dictionary<long, Account> accountDictionary = new Dictionary<long, Account>();
        private string name;
        private long routingNumber;
        // private bool bankAcctDictHasUnsavedChanges = false;
        // private static bool bankDictHasUnsavedChanges = false;

        public static string? UIFeedback;

        private static List<string> stringListOfAccounts;
        public string Name { get => name; }
        public long RoutingNumber { get => routingNumber; }


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
            else
            {
                //Clear contents of file:
                using (FileStream fs = File.Open($@"C:\Users\Allen\code\CS-1410-final-project\Files\{thisBank.Name + "Accounts"}.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    lock (fs)
                    {
                        fs.SetLength(0);
                    }
                }
            }

            StreamWriter fileWriter = new StreamWriter($@"C:\Users\Allen\code\CS-1410-final-project\Files\{thisBank.Name + "Accounts"}.txt");

            foreach (KeyValuePair<long, Account> keyValuePair in thisBank.accountDictionary)
            {
                fileWriter.WriteLine("Account Number:" + keyValuePair.Value.AccountNumber);
                fileWriter.WriteLine("Balance:" + keyValuePair.Value.Balance);
                fileWriter.WriteLine("Holder Name:" + keyValuePair.Value.HolderName);
                fileWriter.WriteLine("End:yes");
            }
            fileWriter.Flush();
            fileWriter.Close();
            // thisBank.bankAcctDictHasUnsavedChanges = false;


        }

        public static Dictionary<long, Account> LoadAcctsFor(Bank thisBank)
        {
            if (File.Exists($@"C:\Users\Allen\code\CS-1410-final-project\Files\{thisBank.Name + "Accounts"}.txt"))
            {
                var accounts = new Dictionary<long, Account>();
                long acctNum = 0;
                decimal balance = 0M;
                string nameOfHolder = "";

                foreach (var line in File.ReadAllLines($@"C:\Users\Allen\code\CS-1410-final-project\Files\{thisBank.Name + "Accounts"}.txt"))
                {
                    var parts = line.Split(':');
                    if (parts[0] == "Account Number")
                    {
                        acctNum = long.Parse(parts[1]);
                    }

                    else if (parts[0] == "Balance")
                    {
                        balance = decimal.Parse(parts[1]);
                    }

                    else if (parts[0] == "Holder Name")
                    {
                        nameOfHolder = parts[1];
                    }

                    else if (parts[0] == "End")
                    {
                        thisBank.AddAccount(new Account(acctNum, balance, nameOfHolder));
                    }
                }

                return accounts;
            }
            else
            {
                return new Dictionary<long, Account>();
            }
        }



        public static void SaveBanks()
        {
            // Note to self:Need to make bool check for unsaved changes

            // if (bankDictHasUnsavedChanges)
            // {

            //Clear contents of file:
            // using (FileStream fs = File.Open(@"C:\Users\Allen\code\CS-1410-final-project\Files\Banks.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            // {
            //     lock (fs)
            //     {
            //         fs.SetLength(0);
            //     }
            // }

            StreamWriter fileWriter = new StreamWriter(@"C:\Users\Allen\code\CS-1410-final-project\Files\Banks.txt");

            foreach (KeyValuePair<string, Bank> keyValuePair in Bank.bankDictionary)
            {
                fileWriter.WriteLine("Bank Name:" + keyValuePair.Key);
                fileWriter.WriteLine("Routing Number:" + keyValuePair.Value.RoutingNumber);
                fileWriter.WriteLine("End:yes");
            }
            fileWriter.Flush();
            fileWriter.Close();
            // bankDictHasUnsavedChanges = false;

            // }
        }

        //---------------DATA STORAGE--------------

        public static void StoreData()
        {
            SaveBanks();
            foreach (var bankKVPair in bankDictionary)
            {
                var currentBank = bankKVPair.Value;
                SaveAccountsFor(currentBank);
                // foreach (var acctKVPair in currentBank.accountDictionary)
                // {
                //     var currentAccount = acctKVPair.Value;
                //     Account.SaveSubAccountsFor(currentAccount);
                //     foreach (var subAcctKVPair in currentAccount.SubAccountDictionary)
                //     {
                //         var currentSubAccount = subAcctKVPair.Value;
                //         SubAccount.SaveCustomCategoriesFor(currentSubAccount);
                //     }
                // }
            }
        }

        public void ChangeStoredData()
        {
            throw new NotImplementedException();
        }

        public static void LoadData()
        {
            bankDictionary = Bank.LoadBanks();
            foreach (var bankKVPair in bankDictionary)
            {
                var currentBank = bankKVPair.Value;
                Bank.LoadAcctsFor(currentBank);
                // foreach (var acctKVPair in currentBank.accountDictionary)
                // {
                //     var currentAccount = acctKVPair.Value;
                //     Account.LoadSubAcctsFor(currentAccount);
                //     foreach (var subAcctKVPair in currentAccount.SubAccountDictionary)
                //     {
                //         var currentSubAccount = subAcctKVPair.Value;
                //         SubAccount.LoadCustomCategoriesFor(currentSubAccount);
                //     }
                // }
            }

        }

        public void DeleteData()
        {
            throw new NotImplementedException();
        }

        public static Dictionary<string, Bank> LoadBanks()
        {
            if (File.Exists(@"C:\Users\Allen\code\CS-1410-final-project\Files\Banks.txt"))
            {
                var banks = new Dictionary<string, Bank>();
                long routingNum = 0;
                string bankName = "";

                foreach (var line in File.ReadAllLines(@"C:\Users\Allen\code\CS-1410-final-project\Files\Banks.txt"))
                {
                    var parts = line.Split(':');
                    if (parts[0] == "Bank Name")
                    {
                        bankName = parts[1];
                    }

                    else if (parts[0] == "Routing Number")
                    {
                        routingNum = long.Parse(parts[2]);
                    }

                    else if (parts[0] == "End")
                    {
                        Bank.bankDictionary.Add(bankName, new Bank(bankName, routingNum));
                    }
                }

                return banks;
            }
            else
            {
                return new Dictionary<string, Bank>();
            }
        }

        // --------- CONSTRUCTORS ----------
        public Bank(string bankName, long routingNum)
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

        public Bank()
        {
            // loading from saved data...

        }

    }
}