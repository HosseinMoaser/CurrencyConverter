using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics;

var logger = NullLogger<CurrencyConversionService>.Instance;
var converter = new CurrencyConversionService(logger);

converter.UpdateConfiguration(new List<CurrencyPair>
    {
      new CurrencyPair("USD", "CAD", 1.34),
      new CurrencyPair("CAD", "GBP", 0.58),
      new CurrencyPair("USD", "EUR", 0.86)
    });

var stopwatch = new Stopwatch();
stopwatch.Start();

for (int i = 0; i < 100000; i++)
{
    var pair = new CurrencyPair("CAD", "EUR", 0);
    var amount = 100;
    var result = converter.Convert(pair, amount);
}

stopwatch.Stop();
Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");