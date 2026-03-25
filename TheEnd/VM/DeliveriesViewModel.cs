using System.Collections.ObjectModel;
using System.Windows;
using Prototip.Models;
using Prototip.Services;

namespace Prototip.VM;

public class DeliveriesViewModel : BaseViewModel
{
    private readonly DataService _data = DataService.Instance;
    
    public ObservableCollection<Delivery> Deliveries { get; set; } = new();
    public ObservableCollection<Supplier> Suppliers { get; set; } = new();
    public ObservableCollection<Product> Products { get; set; } = new();
    public Delivery? SelectedDelivery { get; set; }
    
    public int NewSupplierId { get; set; }
    public DateTime NewDeliveryDate { get; set; } = DateTime.Now;
    
    public int SelectedProductId { get; set; }
    public int NewProductQuantity { get; set; }
    public decimal NewProductPrice { get; set; }
    
    public ObservableCollection<DeliveryProduct> CurrentItems { get; set; } = new();

    public DeliveriesViewModel()
    {
        LoadSuppliers();
        LoadProducts();
        LoadDeliveries();
    }

    public void LoadSuppliers()
    {
        Suppliers.Clear();
        foreach (var sup in _data.GetSuppliers())
            Suppliers.Add(sup);
    }

    public void LoadProducts()
    {
        Products.Clear();
        foreach (var prod in _data.GetProducts())
            Products.Add(prod);
    }

    public void LoadDeliveries()
    {
        Deliveries.Clear();
        var deliveries = _data.GetDeliveries();
        foreach (var d in deliveries)
        {
            d.Items = _data.GetDeliveryProducts().Where(dp => dp.DeliveryId == d.Id).ToList();
            Deliveries.Add(d);
        }
    }

    public string GetProductName(int productId)
    {
        return _data.GetProducts().FirstOrDefault(p => p.Id == productId)?.Name ?? "";
    }

    public string GetSupplierName(int supplierId)
    {
        return _data.GetSuppliers().FirstOrDefault(s => s.Id == supplierId)?.Name ?? "";
    }

    public void AddItem()
    {
        if (SelectedProductId == 0)
        {
            MessageBox.Show("Выберите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (NewProductQuantity <= 0)
        {
            MessageBox.Show("Введите количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (NewProductPrice <= 0)
        {
            MessageBox.Show("Введите цену!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        CurrentItems.Add(new DeliveryProduct
        {
            ProductId = SelectedProductId,
            Quantity = NewProductQuantity,
            PurchasePrice = NewProductPrice
        });
        
        SelectedProductId = 0;
        NewProductQuantity = 0;
        NewProductPrice = 0;
    }

    public void RemoveItem(DeliveryProduct item)
    {
        CurrentItems.Remove(item);
    }

    public decimal GetTotalAmount()
    {
        decimal total = 0;
        foreach (var item in CurrentItems)
            total += item.Quantity * item.PurchasePrice;
        return total;
    }

    public bool SaveDelivery()
    {
        if (NewSupplierId == 0)
        {
            MessageBox.Show("Выберите поставщика!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (CurrentItems.Count == 0)
        {
            MessageBox.Show("Добавьте товары в поставку!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        var delivery = new Delivery
        {
            SupplierId = NewSupplierId,
            DeliveryDate = NewDeliveryDate,
            TotalAmount = GetTotalAmount()
        };
        
        _data.AddDelivery(delivery);
        
        foreach (var item in CurrentItems)
        {
            var deliveryProduct = new DeliveryProduct
            {
                DeliveryId = delivery.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                PurchasePrice = item.PurchasePrice
            };
            _data.AddDeliveryProduct(deliveryProduct);
            
            var currentStock = _data.GetStockQuantity(item.ProductId);
            _data.UpdateStock(item.ProductId, currentStock + item.Quantity);
        }
        
        LoadDeliveries();
        ClearForm();
        return true;
    }

    public void DeleteDelivery(Delivery delivery)
    {
        if (delivery == null) return;
        
        if (MessageBox.Show($"Удалить поставку #{delivery.Id} от {delivery.DeliveryDate:dd.MM.yyyy}?", 
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            _data.DeleteDeliveryProductsByDeliveryId(delivery.Id);
            _data.DeleteDelivery(delivery.Id);
            LoadDeliveries();
        }
    }

    public void ClearForm()
    {
        NewSupplierId = 0;
        NewDeliveryDate = DateTime.Now;
        CurrentItems.Clear();
        SelectedProductId = 0;
        NewProductQuantity = 0;
        NewProductPrice = 0;
    }
}