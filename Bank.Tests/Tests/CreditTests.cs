using Bank.Lib;
using NUnit.Framework;
using System;

namespace Bank.Tests
{
    public class CreditTests
    {
        private BankAccount _bankAccount;
        [SetUp]
        public void SetUp()
        {
            _bankAccount = new BankAccount() { User = new User { Name = "Michał" } };
        }
        [Test]
        public void TestCreditNegativ100()
        {
            _bankAccount.CurrentBalance = 100;
            Assert.Throws<ArgumentOutOfRangeException>(()=> _bankAccount.Credit(-100));
        }
        [Test]
        public void TestCredit100CurrentBalace500()
        {
            _bankAccount.CurrentBalance = 500;

            _bankAccount.Credit(100);

            Assert.AreEqual(600, _bankAccount.CurrentBalance);
        }
    }
}
