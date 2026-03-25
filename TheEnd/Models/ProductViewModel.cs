namespace Prototip.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }
    public string Unit { get; set; } = "шт";
    public int MinQuantity { get; set; }
    public int CategoryId { get; set; }
    public int Quantity { get; set; }
}