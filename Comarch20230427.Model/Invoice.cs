namespace Comarch20230427.Model
{
    public class Invoice : IComparable<Invoice>
    {
        public string Number { get; set; }

        public decimal TotalGrossValue { get; set; }

        public int CompareTo(Invoice? other)
        {
            return Number.CompareTo(other.Number);
        }
    }
}