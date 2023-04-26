using Comarch20230427.Services.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comarch20230427.Services.Tests.Sale
{
    public class VatCalculatorTests
    {
        [TestCase(10, 23, 12.3d)]
        [TestCase(20, 23, 24.6d)]
        [MaxTime(100)]
        public void CalculatePriceShouldReturnCorrectBruttoValue
            (decimal netto, decimal vat, decimal expected)
        {
            //Arrange
            VatCalculator calculator = new VatCalculator();

            //Act
            decimal actual = calculator.CalculatePrice(netto, vat);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(-1)]
        [TestCase(101)]
        public void CalculatePriceShouldThrowsOutOfRangeException(decimal vat)
        {
            //Arrange
            VatCalculator calculator = new VatCalculator();
            decimal netto = 10;

            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculatePrice(netto, vat));

        }

    }
}
