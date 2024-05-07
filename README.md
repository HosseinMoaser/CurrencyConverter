# Currency Conversion Service

## Overview
This repository contains a C# implementation of a currency conversion service. The service is capable of converting between different currencies using provided conversion rates. It can also find the shortest conversion path between two currencies when a direct conversion rate is not available.

## Features
- Configurable conversion rates
- Direct currency conversion
- Indirect currency conversion via the shortest conversion path
- Thread-safe operations
- Optimized for frequent conversions

## Usage
First, create an instance of the `CurrencyConversionService` class. Then, update the conversion configuration with a list of `CurrencyPair` objects, each representing a direct conversion rate between two currencies. Finally, call the `Convert` method to convert an amount from one currency to another.

```csharp
var converter = new CurrencyConversionService();
converter.UpdateConfiguration(new List<CurrencyPair>
{
    new CurrencyPair("USD", "CAD", 1.34),
    new CurrencyPair("CAD", "GBP", 0.58),
    new CurrencyPair("USD", "EUR", 0.86)
});
var pair = new CurrencyPair("CAD", "EUR", 0);
var amount = 100;
var result = converter.Convert(pair, amount);
