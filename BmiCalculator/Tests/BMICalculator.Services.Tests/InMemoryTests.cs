using Autofac;
using BMICalculator.Model.Data;
using BMICalculator.Model.DTO;
using BMICalculator.Model.Model;
using BMICalculator.Model.Repositories;
using FluentAssertions;
using FluentAssertions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator.Services.Tests
{
    public class InMemoryTests
    {
        IContainer ioc;

        [SetUp]
        public void SetUp() 
        {
            RegisterHelper registerHelper = new RegisterHelper();
            var builder = registerHelper.GetContainerBuilder();
            builder.RegisterType<BmiCalculatorFacade>().As<IBmiCalculatorFacade>();
            registerHelper.RegisterDbContext(builder);
            builder.RegisterType<ResultRepository>().As<IResultRepository>();
            ioc = builder.Build();

            InitializeData();
        }

        private void InitializeData()
        {
            using var dbContext = ioc.BeginLifetimeScope().Resolve<ApplicationDbContext>();

            List<BmiMeasurement> bmiMeasurements = new()
            {
                new BmiMeasurement { Id = Guid.NewGuid(), Bmi = 20, BmiClassification = BmiClassification.Normal, Date = 1.March(2023) },
                new BmiMeasurement { Id = Guid.NewGuid(), Bmi = 30, BmiClassification = BmiClassification.Obesity, Date = 27.March(2022) }
            };

            dbContext.BmiMeasurements.AddRange(bmiMeasurements);
            dbContext.SaveChanges();
        }

        [Test]
        [Category("State-1")]
        public async Task SaveResultShouldInsertRecordToDb()
        {
            //Arrange
            BmiMeasurement measurement = new()
            {
                Id = Guid.NewGuid(),
                Bmi = 20,
                BmiClassification = BmiClassification.Normal,
                Date = 28.April(2023),
                Summary = "Not OK"
            };

            var bmiFacade = ioc.Resolve<IBmiCalculatorFacade>();
            using var dbContext = ioc.BeginLifetimeScope().Resolve<ApplicationDbContext>();

            //Act
            await bmiFacade.SaveResult(measurement);

            //Assert
            BmiMeasurement? savedMeasurement = dbContext.BmiMeasurements.FirstOrDefault(x => x.Id == measurement.Id);

            savedMeasurement.Should().NotBeNull();
            savedMeasurement.Id.Should().Be(measurement.Id);
            savedMeasurement.Bmi.Should().Be(measurement.Bmi);
            savedMeasurement.Summary.Should().Be(measurement.Summary);
            savedMeasurement.BmiClassification.Should().Be(measurement.BmiClassification);
        }
    }
}
