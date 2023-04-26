namespace Comarch20230427.Services.Sale;

public class PriceCalculator
{
    public decimal GetCarPrice(string carType)
    {
        string normalizedCarType = carType.Trim().ToLower();

        decimal result = normalizedCarType switch
        {
            "car" => 100000,
            "truck" => 200000,
            "bus" => 300000,
            _ => throw new ArgumentException("Podany typ auta jest nieprawidłowy", nameof(carType)),
        };
            
        return result;
    }
}
