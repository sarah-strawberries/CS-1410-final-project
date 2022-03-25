namespace PersonalFinanceManager
{
    public class Bank : IHasDictionaryList
    {
        private Dictionary<int, Account> accountList = new Dictionary<int, Account>();
        private string name;
        private int routingNumber;

    #region StuffForGettingAccountsList
        private static string accountListAsString;
        private static int count;
    #endregion
        
        public string Name { get => name; }
        public int RoutingNumber { get => routingNumber; }

        public Bank(string bankName, int routingNum)
        {

            if (bankName.Trim().Length <= 1)
            {
                throw new ValueNotAllowedException("Bank name must not be a blank field and also must contain more than one character, excluding spaces.");
            }
            name = bankName.Trim();
            
            
            if (Convert.ToString(routingNum).Length != 9)
            {
                throw new ValueNotAllowedException("Routing number must be 9 digits, and the first digit must not be 0.");
            }
            routingNumber = routingNum;

            // Need to add something in here to make sure there are no banks with the same name or routing numbers
        }

        public string GetBankInfo()
        {
            return String.Format($"Bank Name: {Name} \n \n Routing Number: {RoutingNumber}");
        }

        public static string GetAccountListFor(Bank thisBank)
        {
            // Each instance of Bank contains a private Dictionary<int, Account> that uses an account 
            //   number to access the value, which is a reference to an object of type Account.

            //    for (count = 0, /*some code to iterate through the Accounts in thisBank.Accounts */ count++)
            //    {
            //        // update the accountListAsString variable
            //    }
               return "This method has not been coded yet"; //delete this line after method is complete

               /* The final return value should be a string with the last 4 digits of the account number 
                  (the account number is the key in the Accounts Dictionary), followed by the account type 
                  and the account holder's name for each Account in the referenced Bank object. Each account's 
                  info (acct. number/account type/holder name) should be listed on its own line (using \n to 
                  add a new line inside the string after each account holder's name).

                  Example of what this might look like:

                  XXXXX5896 : Checking account, John Doe
                  XXXXX3869 : Savings account, Jane Doe
               */
        }


        // public static void AddAccountToBank(Bank thisBank, Account thisAccount)
        // {

        // }
    }
}
