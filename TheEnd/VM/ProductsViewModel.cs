using System.Collections.ObjectModel;
using System.Windows;
using Prototip.Models;
using Prototip.Services;

namespace Prototip.VM;

public class ProductsViewModel : BaseViewModel
{
    private readonly DataService _data = DataService.Instance;
    
    public ObservableCollection<ProductViewModel> Products { get; set; } = new();
    public ObservableCollection<Category> Categories { get; set; } = new();
    public Product? SelectedProduct { get; set; }
    
    public string NewName { get; set; } = "";
    public decimal NewPurchasePrice { get; set; }
    public decimal NewRetailPrice { get; set; }
    public string NewUnit { get; set; } = "шт";
    public string NewBarcode { get; set; } = "";
    public int NewMinQuantity { get; set; }
    public int NewCategoryId { get; set; }
    public int NewQuantity { get; set; }

    public ProductsViewModel()
    {
        LoadCategories();
        LoadProducts();
    }

    public void LoadCategories()
    {
        Categories.Clear();
        foreach (var cat in _data.GetCategories())
            Categories.Add(cat);
    }

    public void LoadProducts()
    {
        Products.Clear();
        foreach (var prod in _data.GetProducts())
        {
            var stock = _data.GetStockQuantity(prod.Id);
            Products.Add(new ProductViewModel
            {
                Id = prod.Id,
                Name = prod.Name,
                Barcode = prod.Barcode,
                PurchasePrice = prod.PurchasePrice,
                RetailPrice = prod.RetailPrice,
                Unit = prod.Unit,
                MinQuantity = prod.MinQuantity,
                CategoryId = prod.CategoryId,
                Quantity = stock
            });
        }
    }

    public bool Add()
    {
        if (!Validate()) return false;
        
        var product = new Product
        {
            Name = NewName,
            PurchasePrice = NewPurchasePrice,
            RetailPrice = NewRetailPrice,
            Unit = NewUnit,
            Barcode = NewBarcode,
            MinQuantity = NewMinQuantity,
            CategoryId = NewCategoryId
        };
        
        _data.AddProduct(product);
        _data.UpdateStock(product.Id, NewQuantity);
        LoadProducts();
        Clear();
        return true;
    }

    public bool Update()
    {
        if (SelectedProduct == null)
        {
            MessageBox.Show("Выберите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        if (!Validate()) return false;
        
        SelectedProduct.Name = NewName;
        SelectedProduct.PurchasePrice = NewPurchasePrice;
        SelectedProduct.RetailPrice = NewRetailPrice;
        SelectedProduct.Unit = NewUnit;
        SelectedProduct.Barcode = NewBarcode;
        SelectedProduct.MinQuantity = NewMinQuantity;
        SelectedProduct.CategoryId = NewCategoryId;
        
        _data.UpdateProduct(SelectedProduct);
        _data.UpdateStock(SelectedProduct.Id, NewQuantity);
        LoadProducts();
        Clear();
        return true;
    }

    public bool Delete()
    {
        if (SelectedProduct == null)
        {
            MessageBox.Show("Выберите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        if (MessageBox.Show($"Удалить товар '{SelectedProduct.Name}'?", "Подтверждение", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            _data.DeleteProduct(SelectedProduct.Id);
            LoadProducts();
            Clear();
            return true;
        }
        
        return false;
    }

    public void Select(Product product)
    {
        SelectedProduct = product;
        if (product != null)
        {
            NewName = product.Name;
            NewPurchasePrice = product.PurchasePrice;
            NewRetailPrice = product.RetailPrice;
            NewUnit = product.Unit;
            NewBarcode = product.Barcode;
            NewMinQuantity = product.MinQuantity;
            NewCategoryId = product.CategoryId;
            NewQuantity = _data.GetStockQuantity(product.Id);
        }
    }

    public void Clear()
    {
        NewName = "";
        NewPurchasePrice = 0;
        NewRetailPrice = 0;
        NewUnit = "шт";
        NewBarcode = "";
        NewMinQuantity = 0;
        NewCategoryId = 0;
        NewQuantity = 0;
        SelectedProduct = null;
    }

    private bool Validate()
    {
        if (string.IsNullOrWhiteSpace(NewName))
        {
            MessageBox.Show("Введите название товара!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        if (NewCategoryId == 0)
        {
            MessageBox.Show("Выберите категорию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        if (NewPurchasePrice <= 0)
        {
            MessageBox.Show("Введите закупочную цену!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        if (NewRetailPrice <= 0)
        {
            MessageBox.Show("Введите розничную цену!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        if (NewQuantity < 0)
        {
            MessageBox.Show("Введите количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        return true;
    }
}
/*private int GetProductStock(int productId)
{
    var stock = _data.StockBalances.FirstOrDefault(x => x.ProductId == productId);
    return stock?.Quantity ?? 0;
}*/