namespace PersonalFinanceManager
{
    public class Bank
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

               for (count = 0, /*some code to iterate through the Account references in thisBank.Accounts */ count++)
               {
                   /* some code to set the string variable accountListAsString to equal its current value 
                      plus an additional string (the last 4 digits of the current key plus the value
                      of a variable inside the Account referenced to in the value position)
                   */
               }
               return "This method has not been coded yet"; //delete this line after method is complete

               /* The final return value should be a string with the last 4 digits of the account number 
                  (the account number is the key in the Accounts Dictionary) and the account type for each 
                  Account in the referenced Bank object. Each account's info ( acct. number/account type)
                  should be listed on its own line (using \n to add a new line inside the string after 
                  adding each account number and account type).

                  Example of what this might look like:

                  XXXXX5896 :  \n
                  XXXXX3869 : Savings account
               */
        }


        // public static void AddAccountToBank(Bank thisBank, Account thisAccount)
        // {

        // }
    }
}
