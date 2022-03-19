using System.Text.Json;

namespace Bank.Lib
{
    public class FetchCurrenciesData
    {
        public async Task<List<Currency>> Fetch()
        {
            var currency = new Currency();
            var currencies = new List<Currency>();
            var codes = new string[] { "EUR", "USD", "GBP" };
            using (var httpClient = new HttpClient())
            {
                foreach (var code in codes)
                {
                    try
                    {
                        var currencyJSON = await httpClient.GetStringAsync($"https://api.nbp.pl/api/exchangerates/rates/C/{code}/?format=json");
                        var currencyHelper = JsonSerializer.Deserialize<CurrencyHelper>(currencyJSON);
                        currency.Code = (CurrencyCode)Enum.Parse(typeof(CurrencyCode), currencyHelper.code);
                        currency.BID = currencyHelper.rates[0].bid;
                        currency.EffectiveDate = currencyHelper.rates[0].effectiveDate;
                        currencies.Add(currency);
                        currency = new Currency();
                    }
                    catch (HttpRequestException ex)
                    {
                        throw new HttpRequestException(ex.StackTrace);
                    }
                }

            }
            return currencies;
        }
    }
    internal class CurrencyHelper
    {
        public string code { get; set; }
        public List<RateHelper> rates { get; set; }
    }
    internal class RateHelper
    {
        public decimal bid { get; set; }
        public DateTime effectiveDate { get; set; }
    }

}
