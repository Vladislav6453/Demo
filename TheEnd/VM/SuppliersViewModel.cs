using System.Collections.ObjectModel;
using System.Windows;
using Prototip.Models;
using Prototip.Services;

namespace Prototip.VM;

public class SuppliersViewModel : BaseViewModel
{
    private readonly DataService _data = DataService.Instance;
    
    public ObservableCollection<Supplier> Suppliers { get; set; } = new();
    public Supplier? SelectedSupplier { get; set; }
    
    public string NewName { get; set; } = "";
    public string NewContactPerson { get; set; } = "";
    public string NewPhone { get; set; } = "";
    public string NewAddress { get; set; } = "";
    public string NewInn { get; set; } = "";

    public SuppliersViewModel()
    {
        LoadData();
    }

    public void LoadData()
    {
        Suppliers.Clear();
        foreach (var supplier in _data.GetSuppliers())
            Suppliers.Add(supplier);
    }

    public bool Add()
    {
        if (string.IsNullOrWhiteSpace(NewName))
        {
            MessageBox.Show("Введите название поставщика!", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(NewInn))
        {
            MessageBox.Show("Введите ИНН поставщика!", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        var supplier = new Supplier
        {
            Name = NewName,
            ContactPerson = NewContactPerson,
            Phone = NewPhone,
            Address = NewAddress,
            Inn = NewInn
        };

        _data.AddSupplier(supplier);
        LoadData();
        Clear();
        return true;
    }

    public bool Update()
    {
        if (SelectedSupplier == null)
        {
            MessageBox.Show("Выберите поставщика!", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(NewName))
        {
            MessageBox.Show("Введите название поставщика!", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        SelectedSupplier.Name = NewName;
        SelectedSupplier.ContactPerson = NewContactPerson;
        SelectedSupplier.Phone = NewPhone;
        SelectedSupplier.Address = NewAddress;
        SelectedSupplier.Inn = NewInn;
        
        _data.UpdateSupplier(SelectedSupplier);
        LoadData();
        Clear();
        return true;
    }

    public bool Delete()
    {
        if (SelectedSupplier == null)
        {
            MessageBox.Show("Выберите поставщика!", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        var result = MessageBox.Show($"Удалить поставщика '{SelectedSupplier.Name}'?", 
            "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
        if (result == MessageBoxResult.Yes)
        {
            _data.DeleteSupplier(SelectedSupplier.Id);
            LoadData();
            Clear();
            return true;
        }
        
        return false;
    }

    public void Select(Supplier supplier)
    {
        SelectedSupplier = supplier;
        if (supplier != null)
        {
            NewName = supplier.Name;
            NewContactPerson = supplier.ContactPerson;
            NewPhone = supplier.Phone;
            NewAddress = supplier.Address;
            NewInn = supplier.Inn;
        }
    }

    public void Clear()
    {
        NewName = "";
        NewContactPerson = "";
        NewPhone = "";
        NewAddress = "";
        NewInn = "";
        SelectedSupplier = null;
    }
}