namespace PersonalFinanceManager;

public class FileSystemStorageService : IStorageService
{
    public void StoreData(Dictionary<string, Bank> banks)
    {
        SaveBanks(banks);
        foreach (var bankKVPair in banks)
        {
            var currentBank = bankKVPair.Value;
            this.SaveAccountsFor(currentBank);
            // foreach (var acctKVPair in currentBank.accountDictionary)
            // {
            //     var currentAccount = acctKVPair.Value;
            //     Account.SaveSubAccountsFor(currentAccount);
            //     foreach (var subAcctKVPair in currentAccount.SubAccountDictionary)
            //     {
            //         var currentSubAccount = subAcctKVPair.Value;
            //         SubAccount.SaveCustomCategoriesFor(currentSubAccount);
            //     }
            // }
        }
        SaveTransactions();
    }

    public void LoadData()
    {
        LoadBanks();
        if (Bank.BankDictionary != null)
        {
            foreach (var bankKVPair in Bank.BankDictionary)
            {
                var currentBank = bankKVPair.Value;
                Bank.LoadAcctsFor(currentBank);


                // foreach (var acctKVPair in currentBank.accountDictionary)
                // {
                //     var currentAccount = acctKVPair.Value;
                //     Account.LoadSubAcctsFor(currentAccount);
                //     foreach (var subAcctKVPair in currentAccount.SubAccountDictionary)
                //     {
                //         var currentSubAccount = subAcctKVPair.Value;
                //         SubAccount.LoadCustomCategoriesFor(currentSubAccount);
                //     }
                // }
            }
        }
    }


    private void SaveBanks(Dictionary<string, Bank> banks)
    {
        if (File.Exists($"../Files/Banks.json"))
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(Bank.BankDictionary);
                File.WriteAllText($"../Files/Banks.json", json);
            }
            catch
            {
                ValueNotAllowedException.errorMessage = "Oops! Something went wrong with saving banks.";
            }
        }
        else
        {
            File.Create($"../Files/Banks.json");
            this.SaveBanks(Bank.BankDictionary);
        }
    }

    public void SaveAccountsFor(Bank thisBank)
    {
        if (!(thisBank.AccountDictionary == null))
        {
            if (File.Exists($"../Files/{thisBank.Name + "Accounts"}.json"))
            {
                try
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(thisBank.AccountDictionary);
                    File.WriteAllText($"../Files/{thisBank.Name + "Accounts"}.json", json);
                }
                catch
                {
                    ValueNotAllowedException.errorMessage = "Oops! Something went wrong with saving accounts.";
                }
            }
            else
            {
                File.Create($"../Files/{thisBank.Name + "Accounts"}.json");
                this.SaveAccountsFor(thisBank);
            }
        }
        else
        {
            // nothing to save
        }
    }
    public void SaveTransactions()
    {
        var json = System.Text.Json.JsonSerializer.Serialize(TransactionMaker.AllTransactions);
        File.WriteAllText($"../Files/Transactions.json", json);
    }
    public void LoadTransactions()
    {
        if (File.Exists("../Files/Transactions.json"))
        {
            if (File.ReadAllLines("../Files.Transactions.json").Length != 0)
            {
                var json = File.ReadAllLines("../Files/Transactions.json");
                TransactionMaker.AllTransactions = System.Text.Json.JsonSerializer.Deserialize<List<Tuple<string, decimal, TransactionMaker.TransactionType, DateTime, Account>>(json);
            }
        }
    }
    public void LoadBanks()
    {
        if (File.Exists("../Files/Banks.json"))
        {
            if (File.ReadAllLines("../Files/Banks.json").Length != 0)
            {
                var json = File.ReadAllText($"../Files/Banks.json");
                Bank.BankDictionary = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, Bank>>(json);
            }
        }
    }

}
