using Comarch20230427.Services.Sale;
namespace Comarch20230427.Services.Tests.Sale;

public class PriceCalculatorTests
{
    [TestCase("car", 100000)]
    [TestCase("Truck", 200000)]
    [TestCase("BuS", 300000)]
    public void GetCarPriceShouldReturnValidPriceForCarType(string carType, decimal expected)
    {
        //Arrange
        PriceCalculator priceCalculator = new();

        //Act
        decimal actual = priceCalculator.GetCarPrice(carType);

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestCase("")]
    [TestCase("FooBar")]
    public void GetCarPriceShouldThrowExceptionForInvalidCarTypes(string carType)
    {
        //Arrange
        PriceCalculator priceCalculator = new();

        //Act
        //Assert
        Assert.Throws<ArgumentException>(() => priceCalculator.GetCarPrice(carType));
    }
}
