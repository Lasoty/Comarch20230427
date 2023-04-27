using Comarch20230427.Services.Sale;
using FluentAssertions;

namespace Comarch20230427.Services.FluentTests.Sale;

public class VatCalculatorTests
{
    [TestCase(10, 23, 12.3d)]
    [TestCase(20, 23, 24.6d)]
    [MaxTime(1000)]
    public void CalculatePriceShouldReturnCorrectBruttoValue
        (decimal netto, decimal vat, decimal expected)
    {
        //Arrange
        VatCalculator calculator = new VatCalculator();

        //Act
        decimal actual = calculator.CalculatePrice(netto, vat);

        //Assert
        actual.Should().Be(expected).And.BeGreaterThan(0);
    }
}
