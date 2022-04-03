using NUnit.Framework;
using PersonalFinanceManager;

namespace FinanceManager.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    #region CustomCategoryTests

    [Test]
    public void TestCreateCustomCategory()
    {
        CustomCategory myCustomCategory = new CustomCategory("Tithing");
        Assert.AreEqual(0, myCustomCategory.Balance);
        Assert.AreEqual("Tithing", myCustomCategory.ItemKey);
    }


    #endregion

    #region AccountAndSubAccountTests

    [Test]
    public void TestCreateAccount()
    {
        Account testAccount = new Account("Billy Bob Joe", 12345678);
        Assert.AreEqual("Billy Bob Joe", testAccount.HolderName);
        Assert.AreEqual(12345678, testAccount.AccountNumber);
    }

    // [Test]
    // public void TestCreateSubAccount()
    // {

    // }

    #endregion

    #region BankTests

    [Test]
    public void TestBankNameException()
    {
        try
        {
            Bank bank1 = new Bank(" .      ", 567);
        }
        catch
        {
            System.Console.WriteLine("Initialization of bank1 failed.");
            Assert.Pass();
        }
        Assert.Fail();
    }

    [Test]
    public void TestGetBankInfo()
    {
        Bank bank1 = new Bank("My First Bank", 100345678);
        Assert.AreEqual("Bank Name: My First Bank \n \n Routing Number: 100345678", bank1.GetBankInfo());
    }

    [Test]
    public void TestGetListOfAccountsInBank()
    {
        Bank bank1 = new Bank("Empty Bank", 100345777);
        Assert.AreEqual(null, Bank.GetAccountListFor(bank1));
    }


    #endregion BankTests
    // [Test]
    // public void TestAddAcctToBank()
    // {
    //     Bank bank1 = new Bank("Test Bank", 234567899);

    // }

    // [Test]
    // public void TestAddCustomCategoryToDictionary()
    // {
    //     CustomCategory myCustomCategory = new CustomCategory("Fun money");
    //     SubAccount.AddCustomCategory(myCustomCategory);
    // }
}