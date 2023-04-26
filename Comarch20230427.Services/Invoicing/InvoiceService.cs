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
}
