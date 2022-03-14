namespace Bank.Lib
{
    public class CurrencyService : ICurrencyService
    {
        private readonly FetchCurrenciesData _fetchCurrencyData;
        public CurrencyService(FetchCurrenciesData fetchCurrencyData)
        {
            _fetchCurrencyData = fetchCurrencyData;
        }

        public virtual List<Currency> GetCurrentCurrenciesRate()
        {
            return _fetchCurrencyData.Fetch().Result;
        }
        
    }
}
