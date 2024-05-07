using CurrencyConverter.Abstractions;
using CurrencyConverter.Exceptions;
using CurrencyConverter.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace CurrencyConverter.Services;

public class CurrencyConversionService : ICurrencyConverter
{
    private readonly ConcurrentDictionary<CurrencyPair, double> _conversionRates = new();
    private readonly ConcurrentDictionary<Tuple<string, string>, double> _conversionPaths = new();
    private readonly ILogger _logger;

    public CurrencyConversionService(ILogger<CurrencyConversionService> logger)
    {
        _logger = logger;
    }

    public void ClearConfiguration()
    {
        _conversionRates.Clear();
        _conversionPaths.Clear();
        _logger.LogInformation("Configuration cleared.");
    }

    public void UpdateConfiguration(IEnumerable<CurrencyPair> conversionRates)
    {
        foreach (var rate in conversionRates)
        {
            _conversionRates[rate] = rate.ConversionRate;
        }
        _logger.LogInformation("Configuration updated.");
    }

    public double Convert(CurrencyPair currencyPair, double amount)
    {
        if (currencyPair.BaseCurrency == currencyPair.QuoteCurrency)
            return amount;

        var visited = new HashSet<string>();
        var queue = new Queue<Tuple<string, double>>();
        queue.Enqueue(Tuple.Create(currencyPair.BaseCurrency, amount));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var currentCurrency = current.Item1;
            var currentAmount = current.Item2;

            if (currentCurrency == currencyPair.QuoteCurrency)
                return currentAmount;

            if (visited.Contains(currentCurrency))
                continue;

            visited.Add(currentCurrency);

            foreach (var rate in _conversionRates.Where(x => x.Key.BaseCurrency == currentCurrency || x.Key.QuoteCurrency == currentCurrency))
            {
                var nextCurrency = rate.Key.BaseCurrency == currentCurrency ? rate.Key.QuoteCurrency : rate.Key.BaseCurrency;
                var conversionRate = rate.Key.BaseCurrency == currentCurrency ? rate.Value : 1 / rate.Value;
                var nextAmount = currentAmount * conversionRate;

                queue.Enqueue(Tuple.Create(nextCurrency, nextAmount));
            }
        }

        throw new ConversionPathNotFoundException();
    }

}