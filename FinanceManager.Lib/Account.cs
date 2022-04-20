using System.IO;
namespace PersonalFinanceManager
{
    public class Account : ICategorizableAccount, IAccount
    {
        private decimal balance;
        public virtual decimal Balance
        {
            get => balance;
            private set
            {
                balance = value;
            }
        }

        public decimal AmountToWithdraw = 0M;
        public decimal AmountToDeposit = 0M;

        private long accountNumber;
        public long AccountNumber => accountNumber;


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

        // /// <summary> Please make me check for unsaved changes! </summary>
        // public bool SubAcctListHasUnsavedChanges = false;

        public Dictionary<string, SubAccount> SubAccountDictionary = new Dictionary<string, SubAccount>();
        private List<Tuple<string, decimal, DateTime>> transactions = new List<Tuple<string, decimal, DateTime>>();
        // ------------ METHODS ------------

        public void WithdrawalTransaction()
        {
            if (WithdrawFunds(AmountToWithdraw))
            {
                Bank.UIFeedback = $"Withdrawal of ${AmountToWithdraw} succeeded!";
            }
            else
            {
                throw new ValueNotAllowedException("Insufficient funds.");
            }
        }

        public bool WithdrawFunds(decimal amount)
        {
            if (Balance - amount < 0)
            {
                return false;
            }
            Balance -= amount;
            return true;
        }

        public void DepositFunds(decimal amount)
        {
            Balance += amount;
        }

        public SubAccount GetSubAccount(SubAccount.SubAccountTypes account)
        {
            try
            {
                return this.SubAccountDictionary[account.ToString()];
            }
            catch
            {
                throw new ArgumentException("Sub account does not exist.");
            }
        }

        // ---------- VOID METHODS -----------

        public void AddSubAccount(SubAccount newSubAccountToAdd)
        {
            this.numberOfSubAccounts++;
            try
            {
                this.SubAccountDictionary.Add(newSubAccountToAdd.AccountType.ToString(), newSubAccountToAdd);
            }
            catch (MaximumReachedException)
            {
                //Maximum number of accounts reached...let the user know!
                this.numberOfSubAccounts--;
            }

        }

        public static void SaveSubAccountsFor(Account thisAccount)
        {
            // Note to self: make bool check for unsaved changes

            // Other note to self: Make sure that the file doesn't overwrite itself partially and leave part as it was.
            // Maybe clear the entire file before saving.

            // if (thisAccount.SubAcctListHasUnsavedChanges)

            if (!(File.Exists($@"C:\Users\Allen\code\CS-1410-final-project\Files\{"SubAccountsFor" + thisAccount.ItemKey}.txt")))
            {
                File.Create($@"C:\Users\Allen\code\CS-1410-final-project\Files\{"SubAccountsFor" + thisAccount.ItemKey}.txt");
            }
            else
            {
                //Clear contents of file:
                using (FileStream fs = File.Open($@"C:\Users\Allen\code\CS-1410-final-project\Files\{"CustomCategoriesFor" + thisAccount.ItemKey}.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    lock (fs)
                    {
                        fs.SetLength(0);
                    }
                    fs.Flush();
                    fs.Close();
                }
            }

            StreamWriter fileWriter = new StreamWriter($@"C:\Users\Allen\code\CS-1410-final-project\Files\{"SubAccountsFor" + thisAccount.ItemKey}.txt");

            foreach (KeyValuePair<string, SubAccount> keyValuePair in thisAccount.SubAccountDictionary)
            {
                fileWriter.WriteLine("SubAccount Number:" + keyValuePair.Value.AccountNumber);
                fileWriter.WriteLine("Balance:" + keyValuePair.Value.Balance);
                fileWriter.WriteLine("SubAccount Type:" + keyValuePair.Value.AccountType);
                fileWriter.WriteLine("End:Yes");

            }
            fileWriter.Flush();
            fileWriter.Close();

        }

        public static Dictionary<string, SubAccount> LoadSubAcctsFor(Account thisAccount)
        {
            if (File.Exists($@"C:\Users\Allen\code\CS-1410-final-project\Files\{"SubAccountsFor" + thisAccount.ItemKey}.txt"))
            {
                var subAccounts = new Dictionary<string, SubAccount>();
                long subAcctNum = 0;
                decimal balance = 0M;
                SubAccount.SubAccountTypes subAcctType = SubAccount.SubAccountTypes.Checking;

                foreach (var line in File.ReadAllLines($@"C:\Users\Allen\code\CS-1410-final-project\Files\{"SubAccountsFor" + thisAccount.ItemKey}.txt"))
                {
                    var parts = line.Split(':');
                    if (parts[0] == "SubAccount Number")
                    {
                        subAcctNum = long.Parse(parts[1]);
                    }

                    else if (parts[0] == "Balance")
                    {
                        balance = decimal.Parse(parts[1]);
                    }

                    else if (parts[0] == "SubAccount Type")
                    {
                        subAcctType = (SubAccount.SubAccountTypes)Enum.Parse(typeof(SubAccount.SubAccountTypes), parts[1]);
                    }
                    else if (parts[0] == "End")
                    {
                        thisAccount.AddSubAccount(new SubAccount(subAcctType, balance, subAcctNum, thisAccount));
                    }
                }

                return subAccounts;
            }
            else
            {
                return new Dictionary<string, SubAccount>();
            }
        }

        // public void AddTransaction(Tuple<string, decimal, DateTime> transaction);
        // {
        //     this.transactions.Add(transaction);
        // }

        // -------- CONSTRUCTORS ---------

        public Account(string nameOfHolder, long accountNum)
        {
            if (!(accountNum >= 10000000 && accountNum <= 99999999))
            {
                throw new ValueNotAllowedException("Account number must be exactly 8 digits and must not have 0 as the first digit.");
            }
            else if (nameOfHolder == null || nameOfHolder.Trim() == "")
            {
                throw new ValueNotAllowedException("Account holder name must not be a blank field.");
            }
            accountNumber = accountNum;
            holderName = nameOfHolder;
            balance = 0M;
        }

        public Account(string nameOfHolder, long accountNum, Bank bank)
        {
            if (!(accountNum >= 10000000 && accountNum <= 99999999))
            {
                throw new ValueNotAllowedException("Account number must be exactly 8 digits and must not have 0 as the first digit.");
            }
            if (nameOfHolder == null || nameOfHolder.Trim() == "")
            {
                throw new ValueNotAllowedException("Account holder name must not be a blank field.");
            }
            accountNumber = accountNum;
            holderName = nameOfHolder;
            balance = 0M;
        }

        ///<summary>This constructor is for loading existing accounts.</summary>
        public Account(long accountNum, decimal acctBalance, string nameOfHolder)
        {
            accountNumber = accountNum;
            holderName = nameOfHolder;
            balance = acctBalance;
        }

    }
}
