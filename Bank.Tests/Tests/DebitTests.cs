using Bank.Lib;
using NUnit.Framework;
using System;

namespace Bank.Tests
{
    public class DebitTests
    {
        private BankAccount _bankAccount;
        [SetUp]
        public void SetUp()
        {
            _bankAccount = new BankAccount() { User = new User { Name = "Micha³" } };
        }
        [Test]
        public void DebitTest_CurrentBalance300Debit300()
        {
            _bankAccount.CurrentBalance = 300;

            _bankAccount.Debit(300);

            Assert.AreEqual(0, _bankAccount.CurrentBalance);
        }
        [Test]
        public void DebitTest_CurrentBalance300Debit200()
        {
            _bankAccount.CurrentBalance = 300;

            _bankAccount.Debit(200);

            Assert.AreEqual(100, _bankAccount.CurrentBalance);
        }
        [Test]
        public void DebitTest_CurrentBalances300DebitNegative400()
        {
            _bankAccount.CurrentBalance = 300;

            Assert.Throws<ArgumentOutOfRangeException>(() => _bankAccount.Debit(-400));
        }
        [Test]
        public void DebitTest_CurrentBalances300Debit400()
        {          
            _bankAccount.CurrentBalance = 300;

            Assert.Throws<ArgumentOutOfRangeException>(() => _bankAccount.Debit(400));
        }
        [Test]
        public void DebitTest_CurrentBalances300Debit400OverdraftLimit200()
        {
            _bankAccount.CurrentBalance = 300;
            _bankAccount.OverdraftLimit = 200;

            _bankAccount.Debit(400);

            Assert.AreEqual(100, _bankAccount.CurrentOverdraft);
        }
        [Test]
        public void DebitTest_CurrentBalances700Debit400OverdraftLimit200()
        {
            _bankAccount.CurrentBalance = 700;
            _bankAccount.OverdraftLimit = 200;

            _bankAccount.Debit(900);

            Assert.AreEqual(200, _bankAccount.CurrentOverdraft);
        }
    }
}