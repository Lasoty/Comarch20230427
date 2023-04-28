using Autofac;
using BMICalculator.Model.Data;
using BMICalculator.Model.Repositories;
using BMICalculator.Services.Interfaces;
using BMICalculator.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BMICalculator.Api.Tests
{
    internal class RegisterHelper
    {
        public ContainerBuilder GetContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<BmiCalculatorFacade>().As<IBmiCalculatorFacade>();
            containerBuilder.RegisterType<ResultRepository>().As<IResultRepository>();
            containerBuilder.RegisterType<BmiDeterminator>().As<IBmiDeterminator>();
            containerBuilder.RegisterType<BmiCalculatorFactory>().As<IBmiCalculatorFactory>();
            containerBuilder.RegisterType<ImperialBmiCalculator>().AsSelf();
            containerBuilder.RegisterType<MetricBmiCalculator>().AsSelf();

            return containerBuilder;
        }

        public void RegisterDbContext(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>()
                .WithParameter("options", CreateDbContextOptions())
                .InstancePerLifetimeScope();
        }

        private DbContextOptions<ApplicationDbContext> CreateDbContextOptions()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            return builder.Options;
        }
    }
}
