namespace PersonalFinanceManager;

public class FileSystemStorageService : IStorageService
{
    public void StoreData(Dictionary<string, Bank> banks)
    {
        SaveBanks(banks);
        foreach (var bankKVPair in banks)
        {
            var currentBank = bankKVPair.Value;
            SaveAccountsFor(currentBank);
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
    }

        public void LoadData()
        {
            Bank.bankDictionary = LoadBanks();
            foreach (var bankKVPair in Bank.bankDictionary)
            {
                var currentBank = bankKVPair.Value;
                //Bank.LoadAcctsFor(currentBank);


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


    private void SaveBanks(Dictionary<string, Bank> banks)
    {
        // Note to self:Need to make bool check for unsaved changes

        // if (bankDictHasUnsavedChanges)
        // {

        //Clear contents of file:
        // using (FileStream fs = File.Open(@"..\Files\Banks.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
        // {
        //     lock (fs)
        //     {
        //         fs.SetLength(0);
        //     }
        // }

        StreamWriter fileWriter = new StreamWriter(@"..\Files\Banks.txt");

        foreach (KeyValuePair<string, Bank> keyValuePair in banks)
        {
            fileWriter.WriteLine("Bank Name:" + keyValuePair.Key);
            fileWriter.WriteLine("Routing Number:" + keyValuePair.Value.RoutingNumber);
            fileWriter.WriteLine("End:yes");
        }
        fileWriter.Flush();
        fileWriter.Close();
        // bankDictHasUnsavedChanges = false;

        // }
    }

    public static void SaveAccountsFor(Bank thisBank)
    {
        // Note to self:Need to make bool check for unsaved changes
        // if (thisBank.bankAcctDictHasUnsavedChanges)

        if (!(File.Exists($@"..\Files\{thisBank.Name + "Accounts"}.txt")))
        {
            File.Create($@"..\Files\{thisBank.Name + "Accounts"}.txt");
        }
        else
        {
            //Clear contents of file:
            using (FileStream fs = File.Open($@"..\Files\{thisBank.Name + "Accounts"}.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                lock (fs)
                {
                    fs.SetLength(0);
                }
                fs.Flush();
                fs.Close();
            }
        }

        StreamWriter fileWriter = new StreamWriter($@"..\Files\{thisBank.Name + "Accounts"}.txt");

        foreach (var account in thisBank.Accounts)
        {
            fileWriter.WriteLine("Account Number:" + account.AccountNumber);
            fileWriter.WriteLine("Balance:" + account.Balance);
            fileWriter.WriteLine("Holder Name:" + account.HolderName);
            fileWriter.WriteLine("End:yes");
        }
        fileWriter.Flush();
        fileWriter.Close();
        // thisBank.bankAcctDictHasUnsavedChanges = false;


    }

    public static Dictionary<string, Bank> LoadBanks()
    {
        if (File.Exists(@"..\Files\Banks.txt"))
        {
            var banks = new Dictionary<string, Bank>();
            long routingNum = 0;
            string bankName = "";

            foreach (var line in File.ReadAllLines(@"..\Files\Banks.txt"))
            {
                var parts = line.Split(':');
                if (parts[0] == "Bank Name")
                {
                    bankName = parts[1];
                }

                else if (parts[0] == "Routing Number")
                {
                    routingNum = long.Parse(parts[2]);
                }

                else if (parts[0] == "End")
                {
                    Bank.bankDictionary.Add(bankName, new Bank(bankName, routingNum));
                }
            }

            return banks;
        }
        else
        {
            return new Dictionary<string, Bank>();
        }
    }

}
