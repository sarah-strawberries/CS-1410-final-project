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
            Assert.Fail();
        } 
        Assert.Fail();
    }
}