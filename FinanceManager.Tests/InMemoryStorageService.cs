using System.Collections.Generic;
using PersonalFinanceManager;

namespace FinanceManager.Tests;

public class InMemoryStorageService : IStorageService
{
    private Dictionary<string,Bank> banks;
    
    public void StoreData(Dictionary<string, Bank> banks)
    {
        this.banks = banks;
    }
}
