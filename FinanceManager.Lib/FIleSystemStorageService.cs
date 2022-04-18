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
}
