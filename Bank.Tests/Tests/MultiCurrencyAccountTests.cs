using Bank.Lib;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bank.Tests
{
    public class MultiCurrencyAccountTests
    {
        private MultiCurrencyAccount _account;
        private ICurrencyService _currencyService;
        [SetUp]
        public void SetUp()
        {
            var currencyServiceMock = new Mock<ICurrencyService>();

            currencyServiceMock
                .Setup(data => data.GetCurrentCurrenciesRate())
                .Returns(new List<Currency>()
                {
                    new Currency()
                    {
                        Code = CurrencyCode.USD,
                        BID = 4.4049m,
                        EffectiveDate = new DateTime(2022, 03, 14)
                    },
                    new Currency()
                    {
                        Code = CurrencyCode.EUR,
                        BID = 4.8375m,
                        EffectiveDate = new DateTime(2022, 03, 14)
                    },
                    new Currency()
                    {
                        Code = CurrencyCode.GBP,
                        BID = 5.7631m,
                        EffectiveDate = new DateTime(2022, 03, 14)
                    }
                });

            _currencyService = currencyServiceMock.Object;
            _account = new MultiCurrencyAccount(_currencyService);
        }
        [Test]
        public void CalcualteBalancWhenUserChangeCurrencyTo_USD_Test()
        {
            _account.CurrentBalance = 500;

            ChangeCurrency(CurrencyCode.USD);

            var balanceAfterCurrencyChanged = _account.CurrentBalance;

            Assert.AreEqual(113.50995m, Math.Round(balanceAfterCurrencyChanged, 5));
        }
        [Test]
        public void CalcualteBalancAndOverdraftWhenUserChangeCurrencyTo_USD_Test()
        {
            _account.CurrentBalance = 500;
            _account.OverdraftLimit = 300;

            ChangeCurrency(CurrencyCode.USD);

            var balanceAfterCurrencyChanged = _account.CurrentBalance;
            var overdraftLimitAfterCurrencyChanged = _account.OverdraftLimit;

            Assert.AreEqual(113.50995m, Math.Round(balanceAfterCurrencyChanged, 5));
            Assert.AreEqual(68.10597m, Math.Round(overdraftLimitAfterCurrencyChanged, 5));
        }

        [Test]
        public void UserChangeCurrencyTo_USD_Debit150Test()
        {
            _account.CurrentBalance = 500;
            _account.ChangeCurrency(CurrencyCode.USD);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Debit(150);
            });
        }

        [Test]
        public void CalcualteOverdraftCurrency_GBP_Debit150Test()
        {
            // BID = 5.7631m,
            _account.CurrentBalance = 500;
            _account.OverdraftLimit = 600;

            ChangeCurrency(CurrencyCode.GBP);

            Debit(150);

            var overdraft = _account.CurrentOverdraft;

            Assert.AreEqual(63.24114m, Math.Round(overdraft,5));
        }
        [Test]
        public void CalcualteOverdraftLimitWhenUserChangeCurrency_GBP_Debit150Test()
        {
            // BID = 5.7631m,
            _account.CurrentBalance = 500;
            _account.OverdraftLimit = 300;

            ChangeCurrency(CurrencyCode.GBP);

            var overdraft = _account.CurrentOverdraft;

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                Debit(150);
            });
        }
        [Test]
        public void CalculateBalancWhenUserChangeCurrencyToUSDTest()
        {
            _account.CurrentBalance = 500;

            ChangeCurrency(CurrencyCode.USD);

            var balanceAfterCurrencyChanged = _account.CurrentBalance;

            Assert.AreEqual(113.50995m, Math.Round(balanceAfterCurrencyChanged, 5));
        }
        [Test]
        public void BidValuEquelsZeroTest()
        {
            var currencyServiceMock = new Mock<ICurrencyService>();

            currencyServiceMock
                .Setup(data => data.GetCurrentCurrenciesRate())
                .Returns(new List<Currency>()
                {
                    new Currency() 
                    { 
                        Code = CurrencyCode.USD, 
                        BID = 0, 
                        EffectiveDate = new DateTime(2022, 03, 14)
                    }
                });

            _account = new MultiCurrencyAccount(currencyServiceMock.Object);
            _account.CurrentBalance = 100;
            

            Assert.Throws<DivideByZeroException>(() =>
            {
                CurrencyCode code = CurrencyCode.USD;
                ChangeCurrency(code);
            });
        }

        [Test]
        public void CurrencyNotFoundTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ChangeCurrency(CurrencyCode.HUF);
            });
        }
        // helper functions
        private void ChangeCurrency(CurrencyCode code)
        {
            try
            {
                _account.ChangeCurrency(code);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex.StackTrace);
                throw new ArgumentOutOfRangeException(ex.StackTrace);
            }
            catch (DivideByZeroException ex)
            {
                Debug.WriteLine(ex.StackTrace);
                throw new DivideByZeroException(ex.StackTrace);
            }
            catch(ArgumentNullException ex)
            {
                Debug.WriteLine(ex.StackTrace);
                throw new ArgumentNullException(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                throw new Exception(ex.StackTrace);
            }

        }
        private void Debit(decimal amount)
        {
            try
            {
                _account.Debit(amount);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex.StackTrace);
                throw new ArgumentOutOfRangeException(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                throw new Exception(ex.StackTrace);
            }

        }
    }
}