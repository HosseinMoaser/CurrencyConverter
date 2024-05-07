namespace CurrencyConverter.Abstractions;

public abstract class CurrencyConverterException : Exception
{
    public CurrencyConverterException(string message) : base(message)
    {
        
    }
}
