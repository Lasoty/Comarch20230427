using Comarch20230427.Model;
using Comarch20230427.Services.Invoicing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comarch20230427.Services.Tests.Invoicing
{
    public class InvoiceServiceTests
    {
        IEnumerable<Invoice> Invoices { get; set; }

        [SetUp]
        public void Setup()
        {
            Invoices = new List<Invoice>()
            {
                new Invoice() {Number = "1", TotalGrossValue = 0},
                new Invoice() {Number = "1", TotalGrossValue = 0},
                new Invoice() {Number = "1", TotalGrossValue = 0},
                new Invoice() {Number = "1", TotalGrossValue = 0},
                new Invoice() {Number = "1", TotalGrossValue = 0},
            };
        }

        [Test]
        public void GetAllInvoicesShouldReturnAllInvoices()
        {
            //Arrange
            InvoiceService service = new InvoiceService(Invoices);

            //Act
            var actual = service.GetAllInvoices();

            //Assert
            CollectionAssert.AllItemsAreNotNull(actual);
            CollectionAssert.AreEqual(Invoices, actual);
            CollectionAssert.AllItemsAreInstancesOfType(actual, typeof(Invoice));
            CollectionAssert.Contains(Invoices, Invoices.First());
        }
    }
}
