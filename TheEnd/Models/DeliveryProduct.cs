namespace Prototip.Models;

public class DeliveryProduct
{
    public int Id { get; set; }
    public int DeliveryId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal PurchasePrice { get; set; }
}