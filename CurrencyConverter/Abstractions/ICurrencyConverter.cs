using CurrencyConverter.Models;

namespace CurrencyConverter.Abstractions;

public interface ICurrencyConverter
{
    void ClearConfiguration();
    void UpdateConfiguration(IEnumerable<CurrencyPair> conversionRates);
    double Convert(CurrencyPair currencyPair, double amount);
}
