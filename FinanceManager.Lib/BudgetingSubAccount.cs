namespace PersonalFinanceManager
{
    public class BudgetingSubAccount : Account, IAccount
    {
        public string BudgetCategory{get => budgetCategory;}
        private string budgetCategory;

        public BudgetingSubAccount(string category)
        {
            budgetCategory = category;
        }
        // This class will be a sub-account that users can 
        // create to budget which money is for which purpose,
        // e.g. they could create one budgeting sub-account for 
        // groceries, one for fun money, one for auto insur-
        // ance, and one for a vacation fund. A budgeting
        // sub-account is not an actual account, but a way
        // that users can visually organize their budget.
    }
}