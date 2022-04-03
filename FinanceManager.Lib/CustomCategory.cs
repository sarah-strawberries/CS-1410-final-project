namespace PersonalFinanceManager
{
    public class CustomCategory : ICategorizableAccount
    {
        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = CategoryName;
                // Add code to prevent setting categoryName to an empty string
            }
        }
        private decimal balance;
        public decimal Balance
        {
            get => balance;
            set
            {
                balance = value;
                // Add code to prevent negative balances (maybe more than that)
            }
        }

        public string ItemKey => CategoryName;

        private string categoryName;

        public CustomCategory(string category)
        {
            categoryName = category;
            balance = 0;
        }
    }
}