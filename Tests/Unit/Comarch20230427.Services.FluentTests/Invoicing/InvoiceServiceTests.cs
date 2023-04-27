using Comarch20230427.Model;
using Comarch20230427.Services.Invoicing;
using FluentAssertions;
using FluentAssertions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comarch20230427.Services.FluentTests.Invoicing
{
    public class InvoiceServiceTests
    {
        [Test]
        public void GenerateInvoiceShouldReturnCorrectInvoiceForGivenItems()
        {
            //Arrange
            var items = new List<BasketItem> { 
                new BasketItem 
                {
                    Id = 1,
                    Name = "Test",
                    NetPrice = 10,
                    Tax = 23,
                    Count = 1
                },
                new BasketItem
                {
                    Id = 2,
                    Name = "Test 2",
                    NetPrice = 10,
                    Tax = 7,
                    Count = 2
                },
            };

            InvoiceService service = new InvoiceService(null);
            DateTime actualDate = 27.April(2023);

            // Act
            Invoice actual = service.GenerateInvoice(actualDate ,items.ToArray());

            //Assert
            actual.CreationDate.Should().Be(actualDate);
            actual.Number.Should().NotBeNullOrEmpty().And.StartWith("2023");
            actual.Items.Should().HaveCount(2);
            actual.TotalGrossValue.Should().Be(33.7m);
            actual.TotalNetValue.Should().Be(30);
        }

        [Test]
        public void GenerateInvoiceShouldRasieInvoiceCreatedEvent()
        {
            //Arrange 
            InvoiceService service = new InvoiceService(null);
            using var monitorSubject = service.Monitor();

            var items = new List<BasketItem> {
                new BasketItem
                {
                    Id = 1,
                    Name = "Test",
                    NetPrice = 10,
                    Tax = 23,
                    Count = 1
                }
            };

            Invoice? eventInvoice = null;

            DateTime actualDate = 27.April(2023);
            service.InvoiceCreated += (sender, invoice) =>
            {
                eventInvoice = invoice;
            };

            // Act
            Invoice actual = service.GenerateInvoice(actualDate, items.ToArray());

            //Assert
            monitorSubject.Should().Raise(nameof(InvoiceService.InvoiceCreated));

            eventInvoice.Should().NotBeNull();
        }
    }
}
