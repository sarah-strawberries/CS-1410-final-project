using NUnit.Framework;
using PersonalFinanceManager;

namespace FinanceManager.Tests;

public class AccountTests
{
    [SetUp]
    public void Setup()
    {
    }

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
            System.Console.WriteLine("Initialization of accountWithBadAcctNum failed because of an invalid account number.");

            Assert.Pass();
        }
    }
    
    [Test]
    public void TestDepositFunds()
    {
        Account testAccount = new Account("Pinocchio", 98765432);
        testAccount.DepositFunds(25M);

        Assert.AreEqual(25, testAccount.Balance);
    }

//     [Test]
//     public void TestMakeWithdrawal()
//     {
//         Account testAccount = new Account("Curious George", 12344321);
//         testAccount.WithdrawFunds();
//     }
 }
