using NUnit.Framework;
using PersonalFinanceManager;
using System.Collections.Generic;

namespace FinanceManager.Tests;

public partial class SubAccountTests
{
    public class BankTests
    {
        [SetUp]
        public void Setup()
        {
        }


        // [Test]
        // public void TestAddBanksToBankDictionary()
        // {
        //     Bank bank1 = new Bank("Amazing Bank", 235678910);
        //     Bank bank2 = new Bank("Average Bank", 987654321);
        //     Bank retrievedBank1 = Bank.GetBank("Amazing Bank");
        //     Bank retrievedBank2 = Bank.GetBank("Average Bank");
        // }

        // [Test]
        // public void TestSaveBanks()
        // {
        //     Bank testBank = new Bank("Test Bank", 123456789);
        //     var storageService = new InMemoryStorageService();
        //     storageService.StoreData(Bank.bankDictionary);
        // }

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
        public void TestAddAccountToBank()
        {
            Bank newBank = new Bank("Mountain America Credit Union", 324079555);
            newBank.AddAccount(new Account("Mickey Mouse", 12345678));
            Account retrievedAccount = newBank.GetAccount(12345678);

            Assert.AreEqual("Mickey Mouse", retrievedAccount.HolderName);
        }

        [Test]
        public void TestStoreDataForInMemoryStorageService()
        {
            InMemoryStorageService bogusStorageService = new InMemoryStorageService();
            Dictionary<string, Bank> bogusBankDictionary = new Dictionary<string, Bank>();
            bogusStorageService.StoreData(bogusBankDictionary);
            Assert.Pass();
        }

        [Test]
        public void TestLoadDataForInMemoryStorageService()
        {
            InMemoryStorageService bogusStorageService = new InMemoryStorageService();
            Dictionary<string, Bank> bogusBankDictionary = new Dictionary<string, Bank>();
            bogusStorageService.LoadData();
            Assert.Pass();
        }


        // [Test]
        // public void TestGetListOfAccountsInBank()
        // {
        //     Bank bank1 = new Bank("Empty Bank", 100345777);
        //     Bank bank2 = new Bank("Non-empty Bank", 233344445);
        //     bank2.AddAccount(new Account("Bobby Solofsky", 77888999));

        //     Assert.AreEqual("No accounts to display.", Bank.GetAccountListFor(bank1));
        //     Assert.AreEqual($"   Accounts in Non-empty Bank: \nXXXXX999 : Bobby Solofsky\n", Bank.GetAccountListFor(bank2));
        // }
    }
}