using Comarch20230427.Model;

namespace Comarch20230427.Services.Invoicing;

public class InvoiceService
{
    private readonly IEnumerable<Invoice> invoices;

    public InvoiceService(IEnumerable<Invoice> invoices)
    {
        this.invoices = invoices;
    }

    public IEnumerable<Invoice> GetAllInvoices()
    {
        return invoices;
    }

    public Invoice GenerateInvoice(DateTime invoiceDate, params BasketItem[] items)
    {
        Invoice invoice = new Invoice();
        invoice.CreationDate = invoiceDate;
        invoice.Number = $"{invoiceDate.Year}/{invoiceDate.Month}/12";
        invoice.Items = items.Select(x => new InvoiceItem 
        {
            Count = x.Count,
            GrossValue = (x.NetPrice + (x.NetPrice* (x.Tax / 100))) * x.Count,
            NetValue = x.NetPrice * x.Count,
            Tax = x.Tax,
            Name = x.Name,
        });

        invoice.TotalNetValue = invoice.Items.Sum(x => x.NetValue);
        invoice.TotalGrossValue = invoice.Items.Sum(x => x.GrossValue);

        return invoice;
    }
}
