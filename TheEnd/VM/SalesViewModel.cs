using System.Collections.ObjectModel;
using System.Windows;
using Prototip.Models;
using Prototip.Services;

namespace Prototip.VM;

public class SalesViewModel : BaseViewModel
{
    private readonly DataService _data = DataService.Instance;
    
    public ObservableCollection<Sale> Sales { get; set; } = new();
    public ObservableCollection<Employee> Employees { get; set; } = new();
    public ObservableCollection<Product> Products { get; set; } = new();
    public Sale? SelectedSale { get; set; }
    
    public int SelectedEmployeeId { get; set; }
    public DateTime NewSaleDate { get; set; } = DateTime.Now;
    public string NewPaymentMethod { get; set; } = "Наличные";
    
    public int SelectedProductId { get; set; }
    public int NewSaleQuantity { get; set; }
    
    public ObservableCollection<SaleItem> CurrentItems { get; set; } = new();

    public SalesViewModel()
    {
        try
        {
            LoadEmployees();
            LoadProducts();
            LoadSales();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка инициализации SalesViewModel: {ex.Message}", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public void LoadEmployees()
    {
        Employees.Clear();
        foreach (var emp in _data.GetEmployees())
            Employees.Add(emp);
    }

    public void LoadProducts()
    {
        Products.Clear();
        foreach (var prod in _data.GetProducts())
            Products.Add(prod);
    }

    public void LoadSales()
    {
        Sales.Clear();
        var sales = _data.GetSales();
        foreach (var s in sales)
        {
            s.Items = _data.GetSaleItems().Where(si => si.SaleId == s.Id).ToList();
            Sales.Add(s);
        }
    }

    public void AddItem()
    {
        if (SelectedProductId == 0)
        {
            MessageBox.Show("Выберите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (NewSaleQuantity <= 0)
        {
            MessageBox.Show("Введите количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var product = _data.GetProducts().FirstOrDefault(p => p.Id == SelectedProductId);
        var stock = _data.GetStockQuantity(SelectedProductId);
        
        if (stock < NewSaleQuantity)
        {
            MessageBox.Show($"Недостаточно товара на складе! Доступно: {stock}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var existing = CurrentItems.FirstOrDefault(i => i.ProductId == SelectedProductId);
        if (existing != null)
        {
            existing.Quantity += NewSaleQuantity;
        }
        else
        {
            CurrentItems.Add(new SaleItem
            {
                ProductId = SelectedProductId,
                Quantity = NewSaleQuantity,
                Price = product?.RetailPrice ?? 0
            });
        }
        
        SelectedProductId = 0;
        NewSaleQuantity = 0;
    }

    public void RemoveItem(SaleItem item)
    {
        CurrentItems.Remove(item);
    }

    public decimal GetTotal()
    {
        decimal total = 0;
        foreach (var item in CurrentItems)
            total += item.Quantity * item.Price;
        return total;
    }

    public bool CompleteSale()
    {
        if (SelectedEmployeeId == 0)
        {
            MessageBox.Show("Выберите сотрудника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (CurrentItems.Count == 0)
        {
            MessageBox.Show("Добавьте товары в продажу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        var sale = new Sale
        {
            EmployeeId = SelectedEmployeeId,
            SaleDate = NewSaleDate,
            TotalAmount = GetTotal(),
            PaymentMethod = NewPaymentMethod
        };
        
        _data.AddSale(sale);
        
        foreach (var item in CurrentItems)
        {
            var saleItem = new SaleItem
            {
                SaleId = sale.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            };
            _data.AddSaleItem(saleItem);
            
            var currentStock = _data.GetStockQuantity(item.ProductId);
            _data.UpdateStock(item.ProductId, currentStock - item.Quantity);
        }
        
        LoadSales();
        ClearForm();
        return true;
    }

    public void ClearForm()
    {
        SelectedEmployeeId = 0;
        NewSaleDate = DateTime.Now;
        NewPaymentMethod = "Наличные";
        CurrentItems.Clear();
        SelectedProductId = 0;
        NewSaleQuantity = 0;
    }
}