using System;
using System.Collections.Generic;

public class Converter
{
    // Dictionary to store the cryptocurrency prices in USD
    private Dictionary<string, double> cryptoPrices = new Dictionary<string, double>();

    // Main method
    public static void Main(string[] args) {

        // Create a new instance of the Converter class
        Converter converter = new Converter();

        // Set the price for Bitcoin to $40000
        converter.SetPricePerUnit("Bitcoin", 4000.0);

        // Set the price for Ethereum to $2500
        converter.SetPricePerUnit("Ethereum", 0.0);

        // Convert 1 Bitcoin to Ethereum
        double convertedAmount = converter.Convert("Bitcoin", "Ethereum", 1.0);

        // Print the converted amount
        Console.WriteLine($"1 Bitcoin is equal to {convertedAmount} Ethereum");

    }

    /// <summary>
    /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
    /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
    /// bliver den gamle værdi overskrevet af den nye værdi
    /// </summary>
    /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
    /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
    public void SetPricePerUnit(string currencyName, double price)
    {
        if (price < 0)
        {
            throw new ArgumentException("Prisen kan ikke være negativ.");
        }

        // Add or update the price for the specified cryptocurrency
        cryptoPrices[currencyName] = price;
    }

    /// <summary>
    /// Konverterer fra en kryptovaluta til en anden. 
    /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
    /// </summary>
    /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
    /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
    /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
    /// <returns>Værdien af beløbet i toCurrencyName</returns>
    public double Convert(string fromCurrencyName, string toCurrencyName, double amount)
    {
        if (!cryptoPrices.ContainsKey(fromCurrencyName))
        {
            throw new ArgumentException($"Valutaen {fromCurrencyName} findes ikke.");
        }

        if (!cryptoPrices.ContainsKey(toCurrencyName))
        {
            throw new ArgumentException($"Valutaen {toCurrencyName} findes ikke.");
        }

        // Get the price per unit for both currencies
        double fromCurrencyPrice = cryptoPrices[fromCurrencyName];
        double toCurrencyPrice = cryptoPrices[toCurrencyName];

        if (toCurrencyPrice == 0)
        {
            throw new DivideByZeroException("Prisen på den valuta, der konverteres til, kan ikke være 0.");
        }

        // Convert the amount from the original currency to the target currency
        double convertedAmount = (amount * fromCurrencyPrice) / toCurrencyPrice;

        return convertedAmount;
    }
}
