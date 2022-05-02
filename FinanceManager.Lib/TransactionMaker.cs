namespace PersonalFinanceManager;
public class TransactionMaker
{
    public enum TransactionType
    {
        Withdrawal,
        Deposit
    }
    public static List<Tuple<string, decimal, TransactionType, DateTime, Account>> AllTransactions =  new List<Tuple<string, decimal, TransactionType, DateTime, Account>>();

}