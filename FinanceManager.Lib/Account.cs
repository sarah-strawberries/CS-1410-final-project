using System.IO;
namespace PersonalFinanceManager
{
    public class Account : ICategorizableAccount, IAccount
    {
        private decimal balance;
        public virtual decimal Balance
        {
            get => balance;
            set => balance = value;
        }

        public decimal AmountToWithdraw;
        public decimal AmountToDeposit;

        private List<Tuple<string, decimal, TransactionMaker.TransactionType, DateTime>> transactions = new List<Tuple<string, decimal, TransactionMaker.TransactionType, DateTime>>();
        public List<Tuple<string, decimal, TransactionMaker.TransactionType, DateTime>> Transactions { get => transactions; set => transactions = value; }

        private long accountNumber;
        public long AccountNumber
        {
            get => accountNumber;
            set => accountNumber = value;
        }

        private string holderName;
        public string HolderName
        {
            get => holderName;
            set => holderName = value;
            //     // if the holder's name should need to be changed...
            //     // add some code to constrain this, otherwise the item key will have to change, too
        }

        public string ItemKey
        {
            get => HolderName;
            set => HolderName = value;
        }

        private int numberOfSubAccounts;
        public int NumberOfSubAccounts
        {
            get => numberOfSubAccounts;
            set => numberOfSubAccounts = value;
        }

        // /// <summary> Please make me check for unsaved changes! </summary>
        // public bool SubAcctListHasUnsavedChanges = false;

        public Dictionary<string, SubAccount> SubAccountDictionary = new Dictionary<string, SubAccount>();
        // ------------ METHODS ------------

        //Probably delete this --v

        // public void WithdrawalTransaction()
        // {
        //     if (WithdrawFunds(AmountToWithdraw))
        //     {
        //         Bank.UIFeedback = $"Withdrawal of ${AmountToWithdraw} succeeded!";
        //     }
        //     else
        //     {
        //         throw new ValueNotAllowedException("Insufficient funds.");
        //     }
        // }


        public void WithdrawFunds(decimal amount)
        {
            if (amount > 0)
            {
                if (Balance - amount < 0)
                {
                    throw new ValueNotAllowedException("Insufficient funds to make specified withdrawal. Please check to make sure you typed the amount correctly.");
                }
                else
                {
                    Balance -= amount;
                }
            }
            else if (amount == 0)
            {
                throw new ValueNotAllowedException("Please enter an amount to withdraw.");

            }
            else
            {
                throw new ValueNotAllowedException("You cannot withdraw a negative amount! Try depositing instead.");
            }

        }

        public void DepositFunds(decimal amount)
        {
            if (amount < 0)
            {
                throw new ValueNotAllowedException("You cannot deposit a negative amount! Try withdrawing instead.");
            }
            else if (amount == 0)
            {
                throw new ValueNotAllowedException("Please enter an amount to deposit.");
            }
            else
            {
                Balance += amount;
            }
        }

        public void AddLiveTransaction(string memo, decimal amount, TransactionMaker.TransactionType type)
        {
            // adds a transaction using the current date/time as the date/time
            var transaction = new Tuple<string, decimal, TransactionMaker.TransactionType, DateTime>(memo, amount, type, DateTime.Now);
            transactions.Add(transaction);
            TransactionMaker.AllTransactions.Add(new Tuple<string, decimal, TransactionMaker.TransactionType, DateTime, Account>(memo, amount, type, DateTime.Now, this));
        }

        public void AddPastTransaction(string memo, decimal amount, TransactionMaker.TransactionType type, DateTime date)
        {
            // adds a transaction with a past date/time as the date/time, using the DateTime given as a parameter
            var transaction = new Tuple<string, decimal, TransactionMaker.TransactionType, DateTime>(memo, amount, type, date);
            this.transactions.Add(transaction);
            TransactionMaker.AllTransactions.Add(new Tuple<string, decimal, TransactionMaker.TransactionType, DateTime, Account>(memo, amount, type, DateTime.Now, this));
        }

        public string accountNumberView()
        {
            return $"XXXXX{(this.AccountNumber.ToString()).Substring(4, 4)}";
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

            if (!(File.Exists($"../Files/{"SubAccountsFor" + thisAccount.ItemKey}.txt")))
            {

                File.Create($"../Files/{"SubAccountsFor" + thisAccount.ItemKey}.txt");
            }
            else
            {
                //Clear contents of file:
                using (FileStream fs = File.Open($"../Files/{"SubAccountsFor" + thisAccount.ItemKey}.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    lock (fs)
                    {
                        fs.SetLength(0);
                    }
                    fs.Flush();
                    fs.Close();
                }
            }

            StreamWriter fileWriter = new StreamWriter($"../Files/{"SubAccountsFor" + thisAccount.ItemKey}.txt");

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
            if (File.Exists($"../Files/{"SubAccountsFor" + thisAccount.ItemKey}.txt"))
            {
                var subAccounts = new Dictionary<string, SubAccount>();
                long subAcctNum = 0;
                decimal balance = 0M;
                SubAccount.SubAccountTypes subAcctType = SubAccount.SubAccountTypes.Checking;

                foreach (var line in File.ReadAllLines($"../Files/{"SubAccountsFor" + thisAccount.ItemKey}.txt"))
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

        public Account()
        {
            // for .json deserialization
        }

    }
}
