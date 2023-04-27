using BMICalculator.Model.DTO;
using BMICalculator.Model.Model;
using BMICalculator.Model.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator.Services.Tests
{
    public class ResultServiceTests
    {
        private Mock<IResultRepository> resultRepositoryMock;
        private IResultRepository resultRepository;
        private ResultService resultService;

        [SetUp]
        public void Setup()
        {
            resultRepositoryMock = new Mock<IResultRepository>();
            resultRepository = resultRepositoryMock.Object;

            resultService = new ResultService(resultRepository);
        }

        [Test]
        public void SetRecentOverweightResultShouldChangeStateResultService()
        {
            //Arrange 
            BmiResult bmi = new()
            {
                Bmi = 30,
                BmiClassification = BmiClassification.Overweight,
                Summary = string.Empty,
            };

            //Act
            resultService.SetRecentOverweightResult(bmi);

            //Assert
            resultService.RecentOverweightResult.Should().Be(bmi);
        }

        [Test]
        public async Task SaveUnderweightResultAsyncShouldCallSaveResultOnlyOnce()
        {
            //Arrange
            BmiMeasurement measurement = new();

            //Act
            await resultService.SaveUnderweightResultAsync(measurement);

            //Assert
            resultRepositoryMock.Verify(m => m.SaveResultAsync(It.IsAny<BmiMeasurement>()), Times.Once);
        }
    }
}
