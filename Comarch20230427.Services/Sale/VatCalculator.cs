namespace Comarch20230427.Services.Sale;

public class VatCalculator
{
    public decimal CalculatePrice(decimal netto, decimal vat)
    {
        ValidationVat(vat);
        return netto + (netto * (vat / 100));
    }

    private void ValidationVat(decimal vat)
    {
        if (vat < 0) throw new ArgumentOutOfRangeException("Vat nie może być mniejszy od 0.");
        if (vat > 100) throw new ArgumentOutOfRangeException("Vat nie może być większy od 100.");
    }
}
