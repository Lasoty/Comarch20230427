using Autofac;
using BMICalculator.Model.Repositories;
using BMICalculator.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator.Services.Tests
{
    public class RegisterHelper
    {
        public Mock<IBmiCalculatorFactory> BmiCalculatorFactoryMock { get; set; }

        public Mock<IResultRepository> ResultRepositoryMock { get; set; }

        public ContainerBuilder GetContainerBuilder()
        {
            Mock<IBmiDeterminator> bmiDeterminatorMock = new Mock<IBmiDeterminator>();
            BmiCalculatorFactoryMock = new Mock<IBmiCalculatorFactory>();
            ResultRepositoryMock = new Mock<IResultRepository>();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(bmiDeterminatorMock.Object).As<IBmiDeterminator>();
            containerBuilder.RegisterInstance(BmiCalculatorFactoryMock.Object).As<IBmiCalculatorFactory>();
            containerBuilder.RegisterInstance(ResultRepositoryMock.Object).As<IResultRepository>();

            return containerBuilder;
        }
    }
}
