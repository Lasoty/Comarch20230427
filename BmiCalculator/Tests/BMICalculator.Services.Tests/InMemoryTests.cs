using Autofac;
using AutoFixture;
using AutoFixture.AutoMoq;
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
        IFixture fixture;

        [SetUp]
        public void SetUp() 
        {
            RegisterHelper registerHelper = new RegisterHelper();
            var builder = registerHelper.GetContainerBuilder();
            builder.RegisterType<BmiCalculatorFacade>().As<IBmiCalculatorFacade>();
            registerHelper.RegisterDbContext(builder);
            builder.RegisterType<ResultRepository>().As<IResultRepository>();
            ioc = builder.Build();

            fixture = new Fixture().Customize(new AutoMoqCustomization());
            InitializeData();
        }

        private void InitializeData()
        {
            using var dbContext = ioc.BeginLifetimeScope().Resolve<ApplicationDbContext>();

            List<BmiMeasurement> bmiMeasurements = fixture.CreateMany<BmiMeasurement>(2).ToList();

            dbContext.BmiMeasurements.AddRange(bmiMeasurements);
            dbContext.SaveChanges();
        }

        [Test]
        [Category("State-1")]
        public async Task SaveResultShouldInsertRecordToDb()
        {
            //Arrange
            BmiMeasurement measurement = fixture.Create<BmiMeasurement>();

            var bmiFacade = ioc.Resolve<IBmiCalculatorFacade>();
            using var dbContext = ioc.BeginLifetimeScope().Resolve<ApplicationDbContext>();

            //Act
            await bmiFacade.SaveResult(measurement);

            //Assert
            BmiMeasurement? savedMeasurement = dbContext.BmiMeasurements.FirstOrDefault(x => x.Id == measurement.Id);

            dbContext.BmiMeasurements.ToList().Should().HaveCountGreaterThan(2);
            savedMeasurement.Should().NotBeNull();
            savedMeasurement.Id.Should().Be(measurement.Id);
            savedMeasurement.Bmi.Should().Be(measurement.Bmi);
            savedMeasurement.Summary.Should().Be(measurement.Summary);
            savedMeasurement.BmiClassification.Should().Be(measurement.BmiClassification);
        }
    }
}
