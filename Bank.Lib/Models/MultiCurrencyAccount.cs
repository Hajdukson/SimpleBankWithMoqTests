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
            var currencySelected = _currencyService
                .GetCurrentCurrenciesRate()
                .FirstOrDefault(c => c.Code == code);

            if (currencySelected != null)
            {
                CalculateExchange(currencySelected);
                Currency = currencySelected;
            }
            else
            {
                throw new ArgumentNullException("Currency not found");
            }
        }
        private void CalculateExchange(Currency currency)
        {
            var currencyBID = currency.BID;

            if(currencyBID == null)
            {
                throw new DivideByZeroException();
            }

            CurrentBalance /= currencyBID;
            OverdraftLimit /= currencyBID;
            CurrentOverdraft /= currencyBID;
        }
    }
}
