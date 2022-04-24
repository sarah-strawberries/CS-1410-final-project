using System.Collections.Generic;
using PersonalFinanceManager;

namespace FinanceManager.Tests;

public class InMemoryStorageService : IStorageService
{
    private Dictionary<string,Bank> banks;
    private Dictionary<string,Bank> loadedBanks;

    public void LoadData()
    {
        throw new System.NotImplementedException();
        // loadedBanks = banks;
    }

    public void StoreData(Dictionary<string, Bank> banks)
    {
        this.banks = banks;
    }
}
