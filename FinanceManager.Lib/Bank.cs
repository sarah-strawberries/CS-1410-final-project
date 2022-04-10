namespace PersonalFinanceManager
{
    public class Bank
    {
        public static Dictionary<string, Bank> bankDictionary = new Dictionary<string, Bank>();

        private Dictionary<long, Account> accountDictionary = new Dictionary<long, Account>();
        private string name;
        private int routingNumber;
        private bool bankAcctDictHasUnsavedChanges = false;

        private static string listOfAccountsAsString;
        public string Name { get => name; }
        public int RoutingNumber { get => routingNumber; }


        // ---------- METHODS ----------

        /// <summary> Returns a string value with one account per line containing all the accounts in this Bank's accountDictionary for the purpose of showing to the user </summary>
        public static string GetAccountListFor(Bank thisBank)
        {
            if (thisBank.accountDictionary.Count() == 0)
            {
                return "No accounts to display.";
            }
            listOfAccountsAsString = "";
            listOfAccountsAsString = $"   Accounts in {thisBank.Name}: \n";
            foreach (KeyValuePair<long, Account> entry in thisBank.accountDictionary)
            {
                listOfAccountsAsString += $"XXXXX{(entry.Key.ToString()).Substring(5, 3)} : {entry.Value.HolderName}\n";
                // updates the accountListAsString variable
            }
            // Still need to test the above method

            return listOfAccountsAsString;
        }

        public static void AddAccountToBank(Bank thisBank, string holderName, long accountNum)
        {
            Account acct = new Account(holderName, accountNum, thisBank);
            thisBank.accountDictionary.Add(accountNum, acct);
            thisBank.bankAcctDictHasUnsavedChanges = true;
        }

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

        public static void SaveAccountsFor(Bank thisBank)
        {
            // Note to self:Need to make bool check for unsaved changes

            if (thisBank.bankAcctDictHasUnsavedChanges)
            {
            
                StreamWriter fileWriter = new StreamWriter(@"C:\Users\Allen\code\CS-1410-final-project\Files\Accounts.txt");

                foreach (KeyValuePair<long, Account> keyValuePair in thisBank.accountDictionary)
                {
                    fileWriter.WriteLine("Account Number:" + keyValuePair.Value.AccountNumber);
                    fileWriter.WriteLine("Balance:" + keyValuePair.Value.Balance);
                    fileWriter.WriteLine("Bank:" + keyValuePair.Value.HomeBank.Name);
                    fileWriter.WriteLine("End SubAccount");

                }
                fileWriter.Close();
                
            }
        }


        // --------- CONSTRUCTORS ----------
        public Bank(string bankName, int routingNum)
        {
            #region ConstrainingCode
            if (bankName.Trim().Length <= 1 || bankName == null)
            {
                throw new ValueNotAllowedException("Bank name must not be a blank field and also must contain more than one character.");
            }
            else if (bankDictionary.ContainsKey(bankName))
            {
                throw new ValueNotAllowedException("A bank with that name already exists.");
            }
            name = bankName.Trim();


            if (Convert.ToString(routingNum).Length != 9)
            {
                throw new ValueNotAllowedException("Routing number must be 9 digits, and the first digit must not be 0.");
            }
            // Need to add something in here to make sure there are no banks with the same name or routing numbers

            #endregion
            routingNumber = routingNum;

            bankDictionary.Add(bankName, this);

        }

    }
}


//override ToString()?