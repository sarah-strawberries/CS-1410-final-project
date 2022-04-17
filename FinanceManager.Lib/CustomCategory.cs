namespace PersonalFinanceManager
{
    public class CustomCategory : ICategorizableAccount
    {
        public string CategoryName
        {
            get => categoryName;
            // set
            // {
            //     categoryName = value;
            //     // ...if we change categoryName, it'll also change ItemKey
            // }
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

        //----------- METHODS ------------
        

        //----------- CONSTRUCTORS ------------

        public CustomCategory(string category)
        {
            categoryName = category;
            balance = 0;
        }


        /// <summary>This constructor is for loading existing custom categories only.</summary>
        public CustomCategory(decimal categoryBalance, string name)
        {
            categoryName = name;
            balance = categoryBalance;
        }
    }
}