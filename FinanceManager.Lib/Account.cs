using System.IO;
namespace PersonalFinanceManager
{
    /// <summary> "Account" class contains the following: decimal balance; int accountNumber; string holderName</summary>
    public class Account : ICategorizableAccount, IAccount
    {
        internal decimal balance;
        public virtual decimal Balance
        {
            get => balance;
            private set
            {
                balance += value;
                // Add more constraining code
            }
        }

        private int accountNumber;
        public int AccountNumber => accountNumber;


        private string holderName;
        public string HolderName
        {
            get => holderName;
            // private set
            // {
            //     // if the holder's name should need to be changed...
            //     // add some code to constrain this, otherwise the item key will have to change, too
            //     HolderName = value;
            // }
        }

        public string ItemKey => HolderName;


        /// <summary> Please make me check for unsaved changes! </summary>
        public bool SubAcctListHasUnsavedChanges = false;

        private Dictionary<string, SubAccount> subAccountDictionary = new Dictionary<string, SubAccount>();


        // ---------- VOID METHODS ------------
        public static void AddSubAccount(Account thisAccount, SubAccount newSubAccountToAdd)
        {
            thisAccount.subAccountDictionary.Add("string", newSubAccountToAdd);
        }

        public static void SaveSubAccountsFor(Account thisAccount)
        {
            // Need to make bool check for unsaved changes

            //if (thisAccount.SubAcctListHasUnsavedChanges)
            //{
            if (thisAccount.subAccountDictionary.Count() == 0)
            {
                // *output an error that says "No Accounts to Save"*
            }

            StreamWriter fileWriter = new StreamWriter(@"C:\Users\Allen\code\CS-1410-final-project\Files\SubAccounts.txt");

            foreach (KeyValuePair<string, SubAccount> keyValuePair in thisAccount.subAccountDictionary)
            {
                fileWriter.WriteLine(keyValuePair.Key);

                // Inherited members of SubAccount:
                fileWriter.WriteLine("Balance:" + keyValuePair.Value.Balance);
                fileWriter.WriteLine("AccountNumber:" + keyValuePair.Value.AccountNumber);
                fileWriter.WriteLine("HolderName:" + keyValuePair.Value.ItemKey);

                // Members belonging to SubAccount:
                fileWriter.WriteLine("AccountType:" + keyValuePair.Value.AccountType);

            }
            fileWriter.WriteLine("End of SubAccount list for this Account");
            fileWriter.Close();
            //}

            // uncomment the below after making the bool check for unsaved changes
            // else
            // {
            //     // do nothing because changes are already saved
            // }
        }

        // -------- CONSTRUCTORS ---------
        public Account(string nameOfHolder, int accountNum)
        {
            if (!(accountNum >= 10000000 && accountNum <= 99999999))
            {
                throw new ValueNotAllowedException("ERROR: Account number must be exactly 8 digits and must not have 0 as the first digit.");
            }
            accountNumber = accountNum;
            holderName = nameOfHolder;
            balance = 0M;
        }
    }
}
