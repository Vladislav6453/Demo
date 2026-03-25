namespace Prototip.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Barcode { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }
    public string Unit { get; set; }
    public int MinQuantity { get; set; }
    public int CategoryId { get; set; }
}