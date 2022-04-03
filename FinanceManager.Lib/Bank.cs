namespace PersonalFinanceManager
{
    public class Bank
    {
        private static Dictionary<string, Bank> bankDictionary = new Dictionary<string, Bank>();

        private Dictionary<long, Account> accountDictionary = new Dictionary<long, Account>();
        private string name;
        private int routingNumber;

        private static string listOfAccountsAsString;
        public string Name { get => name; }
        public int RoutingNumber { get => routingNumber; }
        

        // ---------- METHODS ----------

        /// <summary> Returns a string value with one account per line containing all the accounts in this Bank's accountDictionary for the purpose of showing to the user </summary>
        public static string GetAccountListFor(Bank thisBank)
        {
            if (listOfAccountsAsString == null)
            {
                return null;
            }
            listOfAccountsAsString = $"   Accounts in {thisBank.Name}: \n";
            foreach (KeyValuePair<long, Account> entry in thisBank.accountDictionary)
            {
                listOfAccountsAsString += $"XXXXX{(entry.Key.ToString()).Substring(4, 4)} : {entry.Value}, {entry.Value.ItemKey}\n";
                // updates the accountListAsString variable
            }

            return listOfAccountsAsString;
        }

        public static void AddAccountToBank(Bank thisBank, Account thisAccount)
        {
            thisBank.accountDictionary.Add(thisAccount.AccountNumber, thisAccount);
        }

        public Account GetAccount(long accountNum)
        {
            return this.accountDictionary[accountNum];
            // Add some code to make it return an error if the account number is bad
        }

        public string GetBankInfo()
        {
            return String.Format($"Bank Name: {Name} \n \n Routing Number: {RoutingNumber}");
        }


        // --------- CONSTRUCTORS ----------
        public Bank(string bankName, int routingNum)
        {
            #region ConstrainingCode
            if (bankName.Trim().Length <= 1 || bankName == null)
            {
                throw new ValueNotAllowedException("Bank name must not be a blank field and also must contain more than one character.");
            }
            // else if (bankDictionary.Contains(KeyValuePair<bankName.Trim(),Bank thisBank>) )
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