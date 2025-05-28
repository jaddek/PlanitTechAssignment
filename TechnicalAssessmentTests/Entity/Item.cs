namespace TechnicalAssessmentTests.Entity;

public sealed class Item
{
    public string Title { get; init; }
    public int Qty { get; init; }

    private decimal? _price;
    public decimal? Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPriceChanged();
        }
    }

    public decimal? Subtotal { get; private set; }

    public Item(string title, int qty, decimal? price = null)
    {
        Title = title;
        Qty = qty;
        Price = price;
    }

    private void OnPriceChanged()
    {
        if (Price.HasValue)
            Subtotal = Price * Qty;
        else
            Subtotal = null;
    }
}