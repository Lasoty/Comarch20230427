using FluentAssertions;

namespace BMICalculator.Services.Tests
{
    public class MetricBmiCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(50, 180, 14, 18)]
        [TestCase(65, 180, 18, 24)]
        [TestCase(82, 180, 25, 30)]
        [TestCase(110, 180, 31, 100)]
        public void CalculateBmiShouldReturnValidBmi(double weight, double height, double minExpected, double maxExpected)
        {
            //Arrange
            MetricBmiCalculator bmiCalculator = new MetricBmiCalculator();

            //Act
            double actual = bmiCalculator.CalculateBmi(weight, height);

            //Assert
            actual.Should().BePositive().And.BeInRange(minExpected, maxExpected);
        }

        [TestCase(0, 180)]
        [TestCase(80, 0)]
        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        public void CalculateBmiShouldThrowExceptionWhenArgumentNotValid(double weight, double height)
        {
            //Arrange
            MetricBmiCalculator bmiCalculator = new MetricBmiCalculator();

            //Act
            //Assert
            bmiCalculator.Invoking(bc => bc.CalculateBmi(weight, height)).Should().Throw<ArgumentException>()
                .Which.Message.Should().Contain("is not a valid number");
        }
    }
}