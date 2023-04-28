using AutoFixture;
using AutoFixture.AutoMoq;
using BMICalculator.Services.AutofixturePresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator.Services.Tests
{
    [TestFixture]
    public class ExampleTests
    {
        [Test]
        public void TestingGuard()
        {
            Assert.Throws<ArgumentNullException>(() => new Example(null, null));
        }

        [Test]
        public void TestingGuard2()
        {
            Assert.Throws<ArgumentNullException>(() => new Example(new Dependency(), null));
        }

        [Test]
        public void TestingGuard3()
        {
            Assert.Throws<ArgumentNullException>(() => new Example(null, new Dependency()));
        }

        [TestCase(typeof(Example))]
        [TestCase(typeof(AnotherExpample))]
        public void EnsureConstructorArgumentsNotNull(Type type)
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new AutoFixture.Idioms.GuardClauseAssertion(fixture);
            assertion.Verify(type.GetConstructors());
        }
    }
}
