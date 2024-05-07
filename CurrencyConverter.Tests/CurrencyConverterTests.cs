using CurrencyConverter.Exceptions;
using CurrencyConverter.Models;

namespace CurrencyConverter.Tests;

public class CurrencyConverterTests
{
    [Fact]
    public void Test_Convert_SameCurrency_Should_Return_Input_Amount()
    {
        var converter = new CurrencyConverter();
        var pair = new CurrencyPair("USD", "USD", 1);
        var amount = 100;
        var result = converter.Convert(pair, amount);
        Assert.Equal(100, result);
    }

    [Fact]
    public void Test_Convert_DirectConversion_Should_Return_Amount_Multiplied_By_ConversionRate()
    {
        var converter = new CurrencyConverter();
        converter.UpdateConfiguration(new List<CurrencyPair>
        {
            new CurrencyPair("USD", "EUR", 0.86)
        });
        var pair = new CurrencyPair("USD", "EUR", 0.86);
        var amount = 100;
        var result = converter.Convert(pair, amount);
        Assert.Equal(86, result);
    }

    [Fact]
    public void Test_Convert_IndirectConversion_Should_Find_New_Path_For_Conversion()
    {
        var converter = new CurrencyConverter();
        converter.UpdateConfiguration(new List<CurrencyPair>
        {
            new CurrencyPair("USD", "CAD", 1.34),
            new CurrencyPair("CAD", "GBP", 0.58),
            new CurrencyPair("USD", "EUR", 0.86)
        });
        var pair = new CurrencyPair("CAD", "EUR", 0);
        var amount = 100;
        var result = converter.Convert(pair, amount);
        Assert.Equal(64.18, result, 2);
    }

    [Fact]
    public void Test_Convert_NoConversionPath_Should_Return_ConversionPathNotFoundException()
    {
        var converter = new CurrencyConverter();
        converter.UpdateConfiguration(new List<CurrencyPair>
        {
            new CurrencyPair("USD", "CAD", 1.34),
            new CurrencyPair("CAD", "GBP", 0.58)
        });
        var pair = new CurrencyPair("USD", "EUR", 0);
        var amount = 100;
        Assert.ThrowsAsync<ConversionPathNotFoundException>(() => converter.Convert(pair, amount));
    }

}
