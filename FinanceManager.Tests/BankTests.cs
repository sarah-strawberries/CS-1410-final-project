using NUnit.Framework;

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
        catch(ValueNotAllowedException)
        {
            Console.WriteLine("Initialization of bank1 failed");
            Assert.Pass();
        } 
        Assert.Fail();
    }
}