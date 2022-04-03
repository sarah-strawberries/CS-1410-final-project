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

    [Test]
    public void TestAddCustomCategoryToDictionary()
    {
        Account myAccount = new Account("Sarah", 12300000);
        SubAccount mySubAccount = new SubAccount(SubAccount.AccountTypes.Savings, myAccount);
        SubAccount.AddCustomCategory(mySubAccount, "College Savings");
        CustomCategory retrievedCustomCategory = SubAccount.GetCustomCategory(mySubAccount, "College Savings");

        Assert.AreEqual("College Savings", retrievedCustomCategory.CategoryName);
    }
    #endregion



    #region AccountTests

    [Test]
    public void TestCreateAccount()
    {
        Account testAccount = new Account("Billy Bob Joe", 12345678);

        Assert.AreEqual("Billy Bob Joe", testAccount.HolderName);
        Assert.AreEqual(12345678, testAccount.AccountNumber);
    }

    [Test]
    public void TestAccountNumberException()
    {
        try
        {
            Account accountWithBadAcctNum = new Account("Mr. Grinch", 01234567);
        }
        catch
        {
            System.Console.WriteLine("Initialization of accountWithBadAcctNum failed.");

            Assert.Pass();
        }
    }

    #endregion



    #region SubAccountTests

    [Test]
    public void TestCreateSubAccount()
    {
        Account mainAccount = new Account("Jimminy Cricket", 21435984);
        SubAccount jimminyCricketSubAccount = new SubAccount(SubAccount.AccountTypes.Checking, mainAccount);

        Assert.AreEqual(mainAccount, jimminyCricketSubAccount.BaseAccount);
        Assert.AreEqual("Checking", jimminyCricketSubAccount.AccountType.ToString());
        Assert.AreEqual(2143598401, jimminyCricketSubAccount.AccountNumber);
    }

    #endregion



    #region BankTests
    [Test]
    public void TestAddBanksToBankDictionary()
    {
        Bank bank1 = new Bank("Amazing Bank", 235678910);
        Bank bank2 = new Bank("Average Bank", 987654321);
        Bank retrievedBank1 = Bank.GetBank("Amazing Bank");
        Bank retrievedBank2 = Bank.GetBank("Average Bank");
    }


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
        Bank bank2 = new Bank("Non-empty Bank", 233344445);
        Bank.AddAccountToBank(bank2, "Bobby Solofsky", 77888999);

        Assert.AreEqual("No accounts to display.", Bank.GetAccountListFor(bank1));
        Assert.AreEqual($"   Accounts in Non-empty Bank: \nXXXXX999 : Bobby Solofsky\n", Bank.GetAccountListFor(bank2));
    }

    [Test]
    public void TestAddAccountToBank()
    {
        Bank newBank = new Bank("Mountain America Credit Union", 324079555);
        Bank.AddAccountToBank(newBank,"Mickey Mouse", 12345678);
        Account retrievedAccount = newBank.GetAccount(12345678);

        Assert.AreEqual("Mickey Mouse", retrievedAccount.HolderName);
    }

    #endregion BankTests
    // [Test]
    // public void TestAddAcctToBank()
    // {
    //     Bank bank1 = new Bank("Test Bank", 234567899);

    // }


}