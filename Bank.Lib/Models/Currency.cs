namespace Bank.Lib
{
    public class Currency
    {
        public CurrencyCode Code { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal BID { get; set; }
    }
    public enum CurrencyCode 
    { 
        EUR,
        USD,
        GBP,
        HUF
    }
}
