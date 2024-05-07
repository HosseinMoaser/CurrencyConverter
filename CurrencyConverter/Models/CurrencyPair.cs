namespace CurrencyConverter.Models;

public class CurrencyPair
{
    public string BaseCurrency { get; set; }
    public string QuoteCurrency { get; set; }
    public double ConversionRate { get; set; }

    public CurrencyPair(string baseCurrency, string quoteCurrency, double conversionRate)
    {
        BaseCurrency = baseCurrency;
        QuoteCurrency = quoteCurrency;
        ConversionRate = conversionRate;
    }

}
