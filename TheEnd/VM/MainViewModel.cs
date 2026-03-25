using System.Windows;
using Prototip.Views;

namespace Prototip.VM;

public class MainViewModel : BaseViewModel
{
    public void OpenCategories() => new CategoriesWindow().ShowDialog();
    public void OpenProducts() => new ProductsWindow().ShowDialog();
    public void OpenSuppliers() => new SuppliersWindow().ShowDialog();
    public void OpenDeliveries() => new DeliveriesWindow().ShowDialog();
    public void OpenEmployees() => new EmployeesWindow().ShowDialog();
    public void OpenSales() => new SalesWindow().ShowDialog();
    public void Exit() => Application.Current.Shutdown();
}