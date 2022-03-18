using NUnit.Framework;
using PersonalFinanceManager;

namespace FinanceManager.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

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
        Bank bank1 = new Bank("My First Bank", 000345678);
        Assert.AreEqual("Bank Name: My First Bank \n \n Routing Number: 000345678", bank1.GetBankInfo());
    }
}