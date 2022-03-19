namespace Bank.Lib
{
    public class MultiCurrencyAccount : BankAccount
    {
        private readonly ICurrencyService _currencyService;
        public MultiCurrencyAccount(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        public Currency Currency { get; set; }
        public void ChangeCurrency(CurrencyCode code)
        {
            var selectedCurrency = _currencyService
                .GetCurrentCurrenciesRate()
                .FirstOrDefault(c => c.Code == code);

            if (selectedCurrency != null)
            {
                CalculateExchange(selectedCurrency);
                Currency = selectedCurrency;
            }
            else
            {
                throw new ArgumentNullException("Currency not found");
            }
        }
        private void CalculateExchange(Currency currency)
        {
            var currencyBID = currency.BID;

            if (currencyBID == 0)
            {
                throw new DivideByZeroException();
            }

            CurrentBalance /= currencyBID;
            OverdraftLimit /= currencyBID;
            CurrentOverdraft /= currencyBID;
        }
    }
}
