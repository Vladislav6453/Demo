using System.Windows;
using System.Windows.Controls;
using Prototip.Models;
using Prototip.VM;

namespace Prototip.Views;

public partial class DeliveriesWindow : Window
{
    private readonly DeliveriesViewModel _viewModel;

    public DeliveriesWindow()
    {
        InitializeComponent();
        _viewModel = new DeliveriesViewModel();
        
        CmbSupplier.ItemsSource = _viewModel.Suppliers;
        CmbProduct.ItemsSource = _viewModel.Products;
        LstDeliveries.ItemsSource = _viewModel.Deliveries;
        LstItems.ItemsSource = _viewModel.CurrentItems;
        DpDate.SelectedDate = DateTime.Now;
        
        UpdateTotal();
    }

    private void LstDeliveries_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void BtnAddItem_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.SelectedProductId = CmbProduct.SelectedValue as int? ?? 0;
        _viewModel.NewProductQuantity = int.TryParse(TxtQuantity.Text, out var q) ? q : 1;
        _viewModel.NewProductPrice = decimal.TryParse(TxtPrice.Text, out var p) ? p : 0;
        
        _viewModel.AddItem();
        UpdateTotal();
        
        TxtQuantity.Text = "1";
        TxtPrice.Clear();
        CmbProduct.SelectedIndex = -1;
    }

    private void BtnRemoveItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is DeliveryProduct item)
        {
            _viewModel.RemoveItem(item);
            UpdateTotal();
        }
    }

    private void BtnDeleteDelivery_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is Delivery delivery)
        {
            _viewModel.DeleteDelivery(delivery);
        }
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.NewSupplierId = CmbSupplier.SelectedValue as int? ?? 0;
        _viewModel.NewDeliveryDate = DpDate.SelectedDate ?? DateTime.Now;
        
        if (_viewModel.SaveDelivery())
        {
            LstDeliveries.ItemsSource = null;
            LstDeliveries.ItemsSource = _viewModel.Deliveries;
            ClearForm();
        }
    }

    private void BtnClear_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.ClearForm();
        ClearForm();
    }

    private void UpdateTotal()
    {
        TxtTotal.Text = _viewModel.GetTotalAmount().ToString("C");
    }

    private void ClearForm()
    {
        CmbSupplier.SelectedIndex = -1;
        DpDate.SelectedDate = DateTime.Now;
        CmbProduct.SelectedIndex = -1;
        TxtQuantity.Text = "1";
        TxtPrice.Clear();
        LstItems.ItemsSource = null;
        LstItems.ItemsSource = _viewModel.CurrentItems;
        UpdateTotal();
    }
}