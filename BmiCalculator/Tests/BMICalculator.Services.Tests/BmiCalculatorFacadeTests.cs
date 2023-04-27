using BMICalculator.Model.DTO;
using BMICalculator.Model.Repositories;
using BMICalculator.Services.Enums;
using BMICalculator.Services.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator.Services.Tests
{
    public class BmiCalculatorFacadeTests
    {
        private BmiCalculatorFacade bmiCalculatorFacade;
        private Mock<IBmiCalculatorFactory> bmiCalculatorFactoryMock;

        [SetUp]
        public void Setup()
        {
            Mock<IBmiDeterminator> bmiDeterminatorMock = new Mock<IBmiDeterminator>();
            bmiCalculatorFactoryMock = new Mock<IBmiCalculatorFactory>();
            Mock<IResultRepository> resultRepositoryMock = new Mock<IResultRepository>();

            bmiCalculatorFacade = new BmiCalculatorFacade(
                bmiDeterminatorMock.Object,
                bmiCalculatorFactoryMock.Object,
                resultRepositoryMock.Object
                );
        }

        [Test]
        public void GetResultShouldResultUnderwightWhenWeightIsLow()
        {
            //Arrange
            bmiCalculatorFactoryMock.Setup(m => m.CreateCalculator(It.IsAny<UnitSystem>()))
                .Returns(new MetricBmiCalculator());

            //Act
            BmiResult actual = bmiCalculatorFacade.GetResult(50, 200, UnitSystem.Metric);

            //Assert
            actual.Should().NotBeNull();
            actual.BmiClassification.Should().Be(BmiClassification.Underweight);
        }

        [Test]
        public void GetResultShouldCallCreateCalculatorOnlyOnes()
        {
            bmiCalculatorFactoryMock.Setup(x => x.CreateCalculator(It.IsAny<UnitSystem>()))
                .Returns(new MetricBmiCalculator());
            var result = bmiCalculatorFacade.GetResult(100, 200, UnitSystem.Metric);
            bmiCalculatorFactoryMock.Verify(x => x.CreateCalculator(It.IsAny<UnitSystem>()), Times.Once());
        }
    }
}
