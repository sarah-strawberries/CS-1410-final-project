namespace PersonalFinanceManager
{
    public class Bank
    {
        private static Dictionary<string, Bank> bankDictionary = new Dictionary<string, Bank>();


        private Dictionary<int, Account> accountDictionary = new Dictionary<int, Account>();
        private string name;
        private int routingNumber;


        /// <summary> Returns a string value with one account per line containing all the accounts in this Bank's accountDictionary for the purpose of showing to the user </summary>
        public static string GetAccountListFor(Bank thisBank)
        {
            if (list == null)
            {
                return null;
            }
            list = $"   Accounts in {thisBank.Name}: \n";
            foreach (KeyValuePair<int, Account> entry in thisBank.accountDictionary)
            {
                list += $"XXXXX{(entry.Key.ToString()).Substring(4, 4)} : {entry.Value}, {entry.Value.ItemKey}\n";
                // updates the accountListAsString variable
            }

            return list;
        }
        private static string list;
        public string Name { get => name; }
        public int RoutingNumber { get => routingNumber; }

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

        public string GetBankInfo()
        {
            return String.Format("Bank Name: " + Name + $" \n \n Routing Number: {RoutingNumber}");
        }

        // public static string GetAccountListFor(Bank thisBank)
        // {
        //     // Each instance of Bank contains a private Dictionary<int, Account> that uses an account 
        //     //   number to access the value, which is a reference to an object of type Account.


        // }
        //        return "This method has not been coded yet"; //delete this line after method is complete

        //        /* The final return value should be a string with the last 4 digits of the account number 
        //           (the account number is the key in the Accounts Dictionary), followed by the account type 
        //           and the account holder's name for each Account in the referenced Bank object. Each account's 
        //           info (acct. number/account type/holder name) should be listed on its own line (using \n to 
        //           add a new line inside the string after each account holder's name).

        //           Example of what this might look like:

        //           XXXXX5896 : John Doe
        //           XXXXX3869 : Jane Doe
        //        */
        // }


        public static void AddAccountToBank(Bank thisBank, Account thisAccount)
        {

        }
    }
}


//override ToString()?