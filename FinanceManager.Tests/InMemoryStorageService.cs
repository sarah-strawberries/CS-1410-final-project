using System.Collections.Generic;
using PersonalFinanceManager;

namespace FinanceManager.Tests;

public class InMemoryStorageService : IStorageService
{
    private Dictionary<string,Bank> banks;
    private Dictionary<string,Bank> loadedBanks;

    public void LoadData()
    {
        loadedBanks = banks;
    }

    public void StoreData(Dictionary<string, Bank> banks)
    {
        throw new System.NotImplementedException();

        //this.banks = banks;
    }
}
