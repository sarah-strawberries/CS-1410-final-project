namespace PersonalFinanceManager
{
    public class Bank
    {
        private Dictionary<int, Account> Accounts = new Dictionary<int, Account>();
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
            
            //    for (count = 0, /*code to iterate through the Accounts in thisBank.Accounts */ count++)
            //    {

            //    }
               return "This method has not been coded yet"; //delete this line after method is complete
        }
        // public static void AddAccountToBank(Bank thisBank, Account thisAccount)
        // {

        // }
    }
}