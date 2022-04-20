using PersonalFinanceManager;

public interface IStorageService
{
    public void StoreData(Dictionary<string, Bank> banks);
    public void LoadData();
    // public void ChangeStoredData();
    // public void DeleteData();
}