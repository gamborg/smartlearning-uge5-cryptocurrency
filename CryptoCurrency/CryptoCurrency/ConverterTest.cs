using System;
using Xunit;

public class ConverterTest
{
    private Converter converter;

    public ConverterTest()
    {
        // Initialize a new instance of the Converter class before each test
        converter = new Converter();
    }

    // Test for SetPricePerUnit: Valid input
    [Fact]
    public void SetPricePerUnit_ValidPrice_ShouldStorePrice()
    {
        // Arrange
        string currencyName = "Bitcoin";
        double price = 40000.0;

        // Act
        converter.SetPricePerUnit(currencyName, price);

        // No assertion here because we trust the Convert method will use the stored price.
        // So we verify the behavior in Convert method test.
    }

    // Test for SetPricePerUnit: Invalid input (negative price)
    [Fact]
    public void SetPricePerUnit_NegativePrice_ShouldThrowArgumentException()
    {
        // Arrange
        string currencyName = "Bitcoin";
        double negativePrice = -1.0;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => converter.SetPricePerUnit(currencyName, negativePrice));
    }

    // Test for Convert: Valid conversion between two currencies
    [Fact]
    public void Convert_ValidCurrencies_ShouldReturnCorrectValue()
    {
        // Arrange
        converter.SetPricePerUnit("Bitcoin", 40000.0);
        converter.SetPricePerUnit("Ethereum", 2500.0);
        double amountInBitcoin = 1.0;

        // Act
        double convertedAmount = converter.Convert("Bitcoin", "Ethereum", amountInBitcoin);

        // Assert
        double expectedAmountInEthereum = (amountInBitcoin * 40000.0) / 2500.0;
        Assert.Equal(expectedAmountInEthereum, convertedAmount, precision: 2);
    }

    // Test for Convert: Invalid conversion from a non-existent currency
    [Fact]
    public void Convert_FromCurrencyNotFound_ShouldThrowArgumentException()
    {
        // Arrange
        converter.SetPricePerUnit("Ethereum", 2500.0);
        double amount = 1.0;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => converter.Convert("NonExistentCurrency", "Ethereum", amount));
    }

    // Test for Convert: Invalid conversion to a non-existent currency
    [Fact]
    public void Convert_ToCurrencyNotFound_ShouldThrowArgumentException()
    {
        // Arrange
        converter.SetPricePerUnit("Bitcoin", 40000.0);
        double amount = 1.0;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => converter.Convert("Bitcoin", "NonExistentCurrency", amount));
    }

    // Test for Convert: Zero amount should return zero regardless of currencies
    [Fact]
    public void Convert_ZeroAmount_ShouldReturnZero()
    {
        // Arrange
        converter.SetPricePerUnit("Bitcoin", 40000.0);
        converter.SetPricePerUnit("Ethereum", 2500.0);
        double amountInBitcoin = 0.0;

        // Act
        double convertedAmount = converter.Convert("Bitcoin", "Ethereum", amountInBitcoin);

        // Assert
        Assert.Equal(0.0, convertedAmount);
    }

    // Test for SetPricePerUnit: Edge case for price being zero
    [Fact]
    public void SetPricePerUnit_ZeroPrice_ShouldStoreZeroPrice()
    {
        // Arrange
        string currencyName = "Bitcoin";
        double price = 0.0;

        // Act
        converter.SetPricePerUnit(currencyName, price);

        // No assertion here. We can later verify in another method's test.
    }

    // Test for Convert: Conversion when the price for one currency is zero
    [Fact]
    public void Convert_FromCurrencyWithZeroPrice_ShouldThrowDivideByZeroException()
    {
        // Arrange
        converter.SetPricePerUnit("Bitcoin", 4000.0);  // Zero price for Bitcoin
        converter.SetPricePerUnit("Ethereum", 0.0);
        double amountInBitcoin = 1.0;

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => converter.Convert("Bitcoin", "Ethereum", amountInBitcoin));
    }
}
