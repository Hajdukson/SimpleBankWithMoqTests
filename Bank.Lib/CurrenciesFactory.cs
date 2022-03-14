namespace Bank.Lib
{
    public static class CurrenciesFactory
    {
        public static ICurrencyService CurrencyService() => new CurrencyService(new FetchCurrenciesData());
    }
}
