using NUnit.Framework;
using PersonalFinanceManager;

namespace FinanceManager.Tests;

public partial class SubAccountTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestSubAccountNumber()
    {

        Account mainAccount = new Account("Jimminy Cricket", 21435984);
        mainAccount.AddSubAccount(new SubAccount(SubAccount.SubAccountTypes.Checking, mainAccount));

        Assert.AreEqual(2143598401, mainAccount.GetSubAccount(SubAccount.SubAccountTypes.Checking).AccountNumber);
    }
}