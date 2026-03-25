using System.Windows;
using Prototip.VM;

namespace Prototip.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;
    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
    }
    private void BtnCategories_Click(object sender, RoutedEventArgs e) => _viewModel.OpenCategories();
    private void BtnProducts_Click(object sender, RoutedEventArgs e) => _viewModel.OpenProducts();
    private void BtnSuppliers_Click(object sender, RoutedEventArgs e) => _viewModel.OpenSuppliers();
    private void BtnDeliveries_Click(object sender, RoutedEventArgs e) => _viewModel.OpenDeliveries();
    private void BtnEmployees_Click(object sender, RoutedEventArgs e) => _viewModel.OpenEmployees();
    private void BtnSales_Click(object sender, RoutedEventArgs e) => _viewModel.OpenSales();
    private void BtnExit_Click(object sender, RoutedEventArgs e) => _viewModel.Exit();
}