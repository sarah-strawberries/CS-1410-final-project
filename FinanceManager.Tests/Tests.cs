using NUnit.Framework;
using PersonalFinanceManager;

namespace FinanceManager.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

#region BankTests

    [Test]
    public void BankNameExceptionTest()
    {
        try
        {
            Bank bank1 = new Bank (" .      ", 567);
        }
        catch
        {
            System.Console.WriteLine("Initialization of bank1 failed.");
            Assert.Pass();
        } 
        Assert.Fail();
    }

    [Test]
    public void GetBankInfoTest()
    {
        Bank bank1 = new Bank("My First Bank", 100345678);
        Assert.AreEqual("Bank Name: My First Bank \n \n Routing Number: 100345678", bank1.GetBankInfo());
    }

    [Test]
    public void GetListOfAccountsInBankTest()
    {
        Bank bank1 = new Bank("Empty Bank", 100345777);
        Assert.AreEqual(null, Bank.GetAccountListFor(bank1));
    }

    // [Test]
    // public void AddAcctToBankTest()
    // {
    //     Bank bank1 = new Bank("Test Bank", 234567899);

    // }

#endregion BankTests
}