namespace Prototip.Models;

public class Sale
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public List<SaleItem> Items { get; set; } = new List<SaleItem>();
}