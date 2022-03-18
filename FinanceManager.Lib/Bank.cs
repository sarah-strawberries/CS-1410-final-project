namespace PersonalFinanceManager
{
    public class Bank
    {
        private static Dictionary<int, Account> Accounts = new Dictionary<int, Account>();
        private string name;
        private int routingNumber;
        
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
        }

        public string GetBankInfo()
        {
            return String.Format($"Bank Name: {0} \n \n Routing Number: {111111111}", Name, RoutingNumber);
        }
    }
}