using Prototip.Models;

namespace Prototip.Services;

public class DataService
{
    private static DataService? _instance;
    public static DataService Instance => _instance ??= new DataService();

    public List<Category> Categories { get; private set; } = new();
    public List<Product> Products { get; private set; } = new();
    public List<StockBalance> StockBalances { get; private set; } = new();
    public List<Supplier> Suppliers { get; private set; } = new();
    public List<Delivery> Deliveries { get; private set; } = new();
    public List<DeliveryProduct> DeliveryProducts { get; private set; } = new();
    public List<Employee> Employees { get; private set; } = new();
    public List<Sale> Sales { get; private set; } = new();
    public List<SaleItem> SaleItems { get; private set; } = new();

    private int _nextCategoryId = 1;
    private int _nextProductId = 1;
    private int _nextStockId = 1;
    private int _nextSupplierId = 1;
    private int _nextDeliveryId = 1;
    private int _nextDeliveryProductId = 1;
    private int _nextEmployeeId = 1;
    private int _nextSaleId = 1;
    private int _nextSaleItemId = 1;

    private DataService()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Категории
        Categories = new List<Category>
        {
            new() { Id = _nextCategoryId++, Name = "Молочные продукты", Description = "Молоко, сыр, йогурты" },
            new() { Id = _nextCategoryId++, Name = "Бакалея", Description = "Крупы, макароны, мука" },
            new() { Id = _nextCategoryId++, Name = "Напитки", Description = "Соки, вода, газировка" },
            new() { Id = _nextCategoryId++, Name = "Хлебобулочные", Description = "Хлеб, булочки, выпечка" }
        };

        // Товары
        Products = new List<Product>
        {
            new() { Id = _nextProductId++, Name = "Молоко 1л", PurchasePrice = 65, RetailPrice = 85, Unit = "шт", Barcode = "460000000001", MinQuantity = 10, CategoryId = 1 },
            new() { Id = _nextProductId++, Name = "Хлеб белый", PurchasePrice = 35, RetailPrice = 45, Unit = "шт", Barcode = "460000000002", MinQuantity = 5, CategoryId = 4 },
            new() { Id = _nextProductId++, Name = "Сок апельсиновый 1л", PurchasePrice = 90, RetailPrice = 120, Unit = "шт", Barcode = "460000000003", MinQuantity = 8, CategoryId = 3 },
            new() { Id = _nextProductId++, Name = "Сыр Российский", PurchasePrice = 450, RetailPrice = 550, Unit = "кг", Barcode = "460000000004", MinQuantity = 3, CategoryId = 1 },
            new() { Id = _nextProductId++, Name = "Макароны", PurchasePrice = 60, RetailPrice = 85, Unit = "шт", Barcode = "460000000005", MinQuantity = 10, CategoryId = 2 }
        };

        // Складские остатки
        StockBalances = new List<StockBalance>
        {
            new() { Id = _nextStockId++, ProductId = 1, Quantity = 50, LastUpdated = DateTime.Now },
            new() { Id = _nextStockId++, ProductId = 2, Quantity = 30, LastUpdated = DateTime.Now },
            new() { Id = _nextStockId++, ProductId = 3, Quantity = 25, LastUpdated = DateTime.Now },
            new() { Id = _nextStockId++, ProductId = 4, Quantity = 15, LastUpdated = DateTime.Now },
            new() { Id = _nextStockId++, ProductId = 5, Quantity = 40, LastUpdated = DateTime.Now }
        };

        // Поставщики
        Suppliers = new List<Supplier>
        {
            new() { Id = _nextSupplierId++, Name = "ООО Молочные Продукты", ContactPerson = "Иванов Иван", Phone = "+7(999)111-22-33", Address = "г. Москва, ул. Ленина, 1", Inn = "1234567890" },
            new() { Id = _nextSupplierId++, Name = "АО Бакалея", ContactPerson = "Петров Петр", Phone = "+7(999)222-33-44", Address = "г. Москва, ул. Садовая, 2", Inn = "0987654321" },
            new() { Id = _nextSupplierId++, Name = "ООО Напитки", ContactPerson = "Сидоров Сидор", Phone = "+7(999)333-44-55", Address = "г. Москва, ул. Лесная, 3", Inn = "1122334455" }
        };

        // Поставки
        Deliveries = new List<Delivery>
        {
            new() { Id = _nextDeliveryId++, SupplierId = 1, DeliveryDate = DateTime.Now.AddDays(-5), TotalAmount = 4250 },
            new() { Id = _nextDeliveryId++, SupplierId = 2, DeliveryDate = DateTime.Now.AddDays(-3), TotalAmount = 3400 }
        };

        // Товары в поставках
        DeliveryProducts = new List<DeliveryProduct>
        {
            new() { Id = _nextDeliveryProductId++, DeliveryId = 1, ProductId = 1, Quantity = 50, PurchasePrice = 65 },
            new() { Id = _nextDeliveryProductId++, DeliveryId = 1, ProductId = 4, Quantity = 10, PurchasePrice = 450 },
            new() { Id = _nextDeliveryProductId++, DeliveryId = 2, ProductId = 2, Quantity = 30, PurchasePrice = 35 },
            new() { Id = _nextDeliveryProductId++, DeliveryId = 2, ProductId = 5, Quantity = 40, PurchasePrice = 60 }
        };

        // Сотрудники
        Employees = new List<Employee>
        {
            new() { Id = _nextEmployeeId++, FullName = "Администратор", Position = "Директор", Phone = "+7(999)123-45-67", HireDate = DateTime.Now.AddMonths(-12), Login = "admin", Password = "admin", Role = "Admin" },
            new() { Id = _nextEmployeeId++, FullName = "Иванов Иван Иванович", Position = "Кассир", Phone = "+7(999)123-45-68", HireDate = DateTime.Now.AddMonths(-6), Login = "cashier1", Password = "123", Role = "Cashier" },
            new() { Id = _nextEmployeeId++, FullName = "Петрова Мария Сергеевна", Position = "Кассир", Phone = "+7(999)123-45-69", HireDate = DateTime.Now.AddMonths(-4), Login = "cashier2", Password = "123", Role = "Cashier" }
        };

        // Продажи
        Sales = new List<Sale>
        {
            new() { Id = _nextSaleId++, EmployeeId = 2, SaleDate = DateTime.Now.AddHours(-2), TotalAmount = 850, PaymentMethod = "Наличные" },
            new() { Id = _nextSaleId++, EmployeeId = 3, SaleDate = DateTime.Now.AddHours(-1), TotalAmount = 550, PaymentMethod = "Карта" }
        };

        // Позиции продаж
        SaleItems = new List<SaleItem>
        {
            new() { Id = _nextSaleItemId++, SaleId = 1, ProductId = 1, Quantity = 2, Price = 85 },
            new() { Id = _nextSaleItemId++, SaleId = 1, ProductId = 2, Quantity = 1, Price = 45 },
            new() { Id = _nextSaleItemId++, SaleId = 2, ProductId = 3, Quantity = 1, Price = 120 },
            new() { Id = _nextSaleItemId++, SaleId = 2, ProductId = 4, Quantity = 1, Price = 550 }
        };

        UpdateStockFromSales();
    }

    private void UpdateStockFromSales()
    {
        foreach (var item in SaleItems)
        {
            var stock = StockBalances.FirstOrDefault(s => s.ProductId == item.ProductId);
            if (stock != null)
            {
                stock.Quantity -= item.Quantity;
                stock.LastUpdated = DateTime.Now;
            }
        }
    }


    public List<Category> GetCategories() => Categories;
    public void AddCategory(Category item) { item.Id = _nextCategoryId++; Categories.Add(item); }
    public void UpdateCategory(Category item)
    {
        var existing = Categories.FirstOrDefault(x => x.Id == item.Id);
        if (existing != null) { existing.Name = item.Name; existing.Description = item.Description; }
    }
    public void DeleteCategory(int id) { Categories.RemoveAll(x => x.Id == id); }

    // Товары
    public List<Product> GetProducts() => Products;
    public void AddProduct(Product item) { item.Id = _nextProductId++; Products.Add(item); }
    public void UpdateProduct(Product item)
    {
        var existing = Products.FirstOrDefault(x => x.Id == item.Id);
        if (existing != null)
        {
            existing.Name = item.Name;
            existing.PurchasePrice = item.PurchasePrice;
            existing.RetailPrice = item.RetailPrice;
            existing.Unit = item.Unit;
            existing.Barcode = item.Barcode;
            existing.MinQuantity = item.MinQuantity;
            existing.CategoryId = item.CategoryId;
        }
    }
    public void DeleteProduct(int id) { Products.RemoveAll(x => x.Id == id); }

    // Складские остатки
    public List<StockBalance> GetStockBalances() => StockBalances;
    
    // ВАЖНО: Добавляем этот метод!
    public int GetStockQuantity(int productId)
    {
        var stock = StockBalances.FirstOrDefault(x => x.ProductId == productId);
        return stock?.Quantity ?? 0;
    }
    
    public void UpdateStock(int productId, int newQuantity)
    {
        var stock = StockBalances.FirstOrDefault(x => x.ProductId == productId);
        if (stock != null)
        {
            stock.Quantity = newQuantity;
            stock.LastUpdated = DateTime.Now;
        }
        else
        {
            StockBalances.Add(new StockBalance { Id = _nextStockId++, ProductId = productId, Quantity = newQuantity, LastUpdated = DateTime.Now });
        }
    }

    // Поставщики
    public List<Supplier> GetSuppliers() => Suppliers;
    public void AddSupplier(Supplier item) { item.Id = _nextSupplierId++; Suppliers.Add(item); }
    public void UpdateSupplier(Supplier item)
    {
        var existing = Suppliers.FirstOrDefault(x => x.Id == item.Id);
        if (existing != null)
        {
            existing.Name = item.Name;
            existing.ContactPerson = item.ContactPerson;
            existing.Phone = item.Phone;
            existing.Address = item.Address;
            existing.Inn = item.Inn;
        }
    }
    public void DeleteSupplier(int id) { Suppliers.RemoveAll(x => x.Id == id); }

    // Поставки
    public List<Delivery> GetDeliveries() => Deliveries;
    public void AddDelivery(Delivery item) { item.Id = _nextDeliveryId++; Deliveries.Add(item); }
    public void DeleteDelivery(int id) { Deliveries.RemoveAll(x => x.Id == id); }

    // Товары в поставках
    public List<DeliveryProduct> GetDeliveryProducts() => DeliveryProducts;
    public void AddDeliveryProduct(DeliveryProduct item) { item.Id = _nextDeliveryProductId++; DeliveryProducts.Add(item); }
    public void DeleteDeliveryProductsByDeliveryId(int deliveryId) { DeliveryProducts.RemoveAll(x => x.DeliveryId == deliveryId); }

    // Сотрудники
    public List<Employee> GetEmployees() => Employees;
    public void AddEmployee(Employee item) { item.Id = _nextEmployeeId++; Employees.Add(item); }
    public void UpdateEmployee(Employee item)
    {
        var existing = Employees.FirstOrDefault(x => x.Id == item.Id);
        if (existing != null)
        {
            existing.FullName = item.FullName;
            existing.Position = item.Position;
            existing.Phone = item.Phone;
            existing.HireDate = item.HireDate;
            existing.Login = item.Login;
            existing.Password = item.Password;
            existing.Role = item.Role;
        }
    }
    public void DeleteEmployee(int id) { Employees.RemoveAll(x => x.Id == id); }
    public Employee? Authenticate(string login, string password) => Employees.FirstOrDefault(x => x.Login == login && x.Password == password);

    // Продажи
    public List<Sale> GetSales() => Sales;
    public void AddSale(Sale item) { item.Id = _nextSaleId++; Sales.Add(item); }
    public void DeleteSale(int id) { Sales.RemoveAll(x => x.Id == id); }

    // Позиции продаж
    public List<SaleItem> GetSaleItems() => SaleItems;
    public void AddSaleItem(SaleItem item) { item.Id = _nextSaleItemId++; SaleItems.Add(item); }
    public void DeleteSaleItemsBySaleId(int saleId) { SaleItems.RemoveAll(x => x.SaleId == saleId); }

    // Вспомогательные методы для получения названий
    public string GetCategoryName(int id) => Categories.FirstOrDefault(x => x.Id == id)?.Name ?? "";
    public string GetProductName(int id) => Products.FirstOrDefault(x => x.Id == id)?.Name ?? "";
    public string GetSupplierName(int id) => Suppliers.FirstOrDefault(x => x.Id == id)?.Name ?? "";
    public string GetEmployeeName(int id) => Employees.FirstOrDefault(x => x.Id == id)?.FullName ?? "";

}