using Bank.Lib;
using NUnit.Framework;

namespace Bank.Tests.Tests
{
    public class SelectCurrencyTests
    {
        private ICurrencyService _currencyService;
        private MultiCurrencyAccount _currencyAccount;
        [SetUp]
        public void SetUp()
        {
            _currencyService = new CurrencyService(new FetchCurrenciesData());
            _currencyAccount = new MultiCurrencyAccount(_currencyService);
        }
        [Test]
        public void EURExistTest()
        {
            _currencyAccount.ChangeCurrency(CurrencyCode.EUR);
            var selectedCurrencyCode = _currencyAccount.Currency.Code;

            Assert.AreEqual(CurrencyCode.EUR, selectedCurrencyCode);
        }
        [Test]
        public void USDExistTest()
        {
            _currencyAccount.ChangeCurrency(CurrencyCode.USD);
            var selectedCurrencyCode = _currencyAccount.Currency.Code;

            Assert.AreEqual(CurrencyCode.USD, selectedCurrencyCode);
        }
        [Test]
        public void GBPExistTest()
        {
            _currencyAccount.ChangeCurrency(CurrencyCode.GBP);
            var selectedCurrencyCode = _currencyAccount.Currency.Code;

            Assert.AreEqual(CurrencyCode.GBP, selectedCurrencyCode);
        }

    }
}
