namespace Prototip.Models;

public class Delivery
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<DeliveryProduct> Items { get; set; } = new List<DeliveryProduct>();
}