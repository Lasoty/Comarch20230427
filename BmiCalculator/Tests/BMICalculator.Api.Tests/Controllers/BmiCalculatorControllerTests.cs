using Autofac;
using BMICalculator.Api.Controllers;
using BMICalculator.Model.Data;
using BMICalculator.Model.DTO;
using BMICalculator.Model.Model;
using BMICalculator.Model.Repositories;
using BMICalculator.Services;
using FluentAssertions.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMICalculator.Services.Enums;

namespace BMICalculator.Api.Tests.Controllers
{
    [TestFixture]
    public class BmiCalculatorControllerTests
    {
        private IContainer ioc;

        [SetUp]
        public void SetUp() 
        {
            RegisterHelper registerHelper = new RegisterHelper();
            var builder = registerHelper.GetContainerBuilder();
            builder.RegisterType<BmiCalculatorFacade>().As<IBmiCalculatorFacade>();
            registerHelper.RegisterDbContext(builder);

            builder.RegisterType<BmiCalculatorController>().AsSelf();
            ioc = builder.Build();
        }

        [Test]
        public async Task CalculateShouldInsertRecordToDbWhenBmiIsObesity()
        {
            //Arrange
            double weight = 160;
            double height = 170;
            UnitSystem unit = UnitSystem.Metric;

            var controller = ioc.Resolve<BmiCalculatorController>();
            using var dbContext = ioc.BeginLifetimeScope().Resolve<ApplicationDbContext>();

            //Act
            var result = controller.Calculate(weight, height, unit);

            //Assert
            dbContext.BmiMeasurements.Any().Should().BeTrue();
            var savedMeasurement = dbContext.BmiMeasurements.OrderByDescending(x => x.Date).FirstOrDefault();

            savedMeasurement.Should().NotBeNull();
            savedMeasurement.Bmi.Should().BeInRange(29, 100);
            savedMeasurement.Summary.Should().NotBeNullOrEmpty();
            savedMeasurement.BmiClassification.Should().BeOneOf(BmiClassification.Obesity, BmiClassification.ExtremeObesity);
        }
    }
}
