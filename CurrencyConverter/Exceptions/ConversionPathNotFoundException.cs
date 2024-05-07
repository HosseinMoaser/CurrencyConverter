using CurrencyConverter.Abstractions;

namespace CurrencyConverter.Exceptions;

public class ConversionPathNotFoundException : CurrencyConverterException
{
    public ConversionPathNotFoundException() : base("Conversion Path Not Found Between Base and Quote Currency...!")
    {
    }
}
