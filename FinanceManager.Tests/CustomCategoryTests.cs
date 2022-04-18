using NUnit.Framework;
using PersonalFinanceManager;

namespace FinanceManager.Tests;

public class CustomCategoryTests
{
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void TestCreateCustomCategory()
    {
        CustomCategory myCustomCategory = new CustomCategory("Tithing");

        Assert.AreEqual(0, myCustomCategory.Balance);
        Assert.AreEqual("Tithing", myCustomCategory.ItemKey);
    }

    [Test]
    public void TestAddCustomCategoryToDictionary()
    {
        Account myAccount = new Account("Sarah", 12300000);
        SubAccount mySubAccount = new SubAccount(SubAccount.SubAccountTypes.Savings, myAccount);
        mySubAccount.CreateAndAddCustomCategory("College Savings");
        CustomCategory retrievedCustomCategory = mySubAccount.GetCustomCategory("College Savings");

        Assert.AreEqual("College Savings", retrievedCustomCategory.CategoryName);
    }
}
