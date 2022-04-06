using System.IO;
namespace PersonalFinanceManager
{
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

        private long accountNumber;
        public long AccountNumber => accountNumber;
        private Bank homeBank;
        public Bank HomeBank => homeBank;

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
        private int numberOfSubAccounts = 0;
        public int NumberOfSubAccounts => numberOfSubAccounts;



        /// <summary> Please make me check for unsaved changes! </summary>
        public bool SubAcctListHasUnsavedChanges = false;

        private Dictionary<string, SubAccount> subAccountDictionary = new Dictionary<string, SubAccount>();


        // ---------- VOID METHODS ------------
        public static void AddSubAccount(Account thisAccount, SubAccount newSubAccountToAdd)
        {
            thisAccount.numberOfSubAccounts++;
            try
            {
                thisAccount.subAccountDictionary.Add(newSubAccountToAdd.AccountType.ToString(), newSubAccountToAdd);
            }
            catch(MaximumReachedException)
            {
                //Maximum number of accounts reached...let the user know!
                thisAccount.numberOfSubAccounts --;
            }

        }

        public static void SaveSubAccountsFor(Account thisAccount)
        {
            // Note to self:Need to make bool check for unsaved changes

            if (thisAccount.SubAcctListHasUnsavedChanges)
            {

                StreamWriter fileWriter = new StreamWriter(@"C:\Users\Allen\code\CS-1410-final-project\Files\SubAccounts.txt");

                foreach (KeyValuePair<string, SubAccount> keyValuePair in thisAccount.subAccountDictionary)
                {
                    fileWriter.WriteLine("Account Number:" + keyValuePair.Value.AccountNumber);
                    fileWriter.WriteLine("Account Type:" + keyValuePair.Key);
                    fileWriter.WriteLine("Balance:" + keyValuePair.Value.Balance);
                    fileWriter.WriteLine("End SubAccount");

                }
                fileWriter.Close();

            }
        }

        // -------- CONSTRUCTORS ---------

        /// <summary> **This constructor for testing only** </summary>
        public Account(string nameOfHolder, long accountNum)
        {
            if (!(accountNum >= 10000000 && accountNum <= 99999999))
            {
                throw new ValueNotAllowedException("ERROR: Account number must be exactly 8 digits and must not have 0 as the first digit.");
            }
            accountNumber = accountNum;
            holderName = nameOfHolder;
            balance = 0M;
        }
        public Account(string nameOfHolder, long accountNum, Bank bank)
        {
            if (!(accountNum >= 10000000 && accountNum <= 99999999))
            {
                throw new ValueNotAllowedException("ERROR: Account number must be exactly 8 digits and must not have 0 as the first digit.");
            }
            homeBank = bank;
            accountNumber = accountNum;
            holderName = nameOfHolder;
            balance = 0M;
        }
    }
}
