using Autofac;
using BMICalculator.Model.DTO;
using BMICalculator.Model.Model;
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
        private Mock<IBmiCalculatorFactory> bmiCalculatorFactoryMock;
        private Mock<IResultRepository> resultRepositoryMock;
        private IContainer ioc;
        private IBmiCalculatorFacade bmiCalculatorFacade;

        [SetUp]
        public void Setup()
        {
            RegisterHelper registerHelper = new RegisterHelper();
            ContainerBuilder builder = registerHelper.GetContainerBuilder();

            bmiCalculatorFactoryMock = registerHelper.BmiCalculatorFactoryMock;
            resultRepositoryMock = registerHelper.ResultRepositoryMock;

            builder.RegisterType<BmiCalculatorFacade>().As<IBmiCalculatorFacade>();
            ioc = builder.Build();
            bmiCalculatorFacade = ioc.Resolve<IBmiCalculatorFacade>();
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

        [Test]
        public async Task SaveResultShouldSaveCorrectObject()
        {
            //Arrange
            List<BmiMeasurement> db = new();

            resultRepositoryMock.Setup(m => m.SaveResultAsync(It.IsAny<BmiMeasurement>()))
                .Callback<BmiMeasurement>(input => { db.Add(input); })
                .Returns(Task.CompletedTask);

            resultRepositoryMock.Setup(m => m.GetAll())
                .Returns(db.AsQueryable());

            //Act
            await bmiCalculatorFacade.SaveResult(new BmiMeasurement { Bmi = 15 });

            //Assert
            db.Should().NotBeNull().And.HaveCountGreaterThan(0);
            var dbResult = resultRepositoryMock.Object.GetAll();
            var result = db.First();

            result.BmiClassification.Should().Be(BmiClassification.Underweight);
            dbResult.Should().BeEquivalentTo(db);
        }
    }
}
