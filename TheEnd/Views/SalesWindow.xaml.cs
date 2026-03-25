using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Prototip.Models;
using Prototip.Services;
using Prototip.VM;

namespace Prototip.Views;

public partial class SalesWindow : Window
{
    private readonly SalesViewModel _viewModel;
    private readonly DataService _data = DataService.Instance;

    public SalesWindow()
    {
        try
        {
            InitializeComponent();
            
            _viewModel = new SalesViewModel();
            
            // Привязываем данные
            CmbEmployee.ItemsSource = _viewModel.Employees;
            CmbProduct.ItemsSource = _viewModel.Products;
            LstSales.ItemsSource = _viewModel.Sales;
            LstItems.ItemsSource = _viewModel.CurrentItems;
            DpDate.SelectedDate = DateTime.Now;
            
            UpdateTotal();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при загрузке окна продаж: {ex.Message}", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            Close();
        }
    }

    private void LstSales_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Для просмотра выбранной продажи (опционально)
    }

    private void BtnAddItem_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _viewModel.SelectedProductId = CmbProduct.SelectedValue as int? ?? 0;
            _viewModel.NewSaleQuantity = int.TryParse(TxtQuantity.Text, out var q) ? q : 1;
            
            _viewModel.AddItem();
            UpdateTotal();
            
            TxtQuantity.Text = "1";
            CmbProduct.SelectedIndex = -1;
            
            // Обновляем список товаров в чеке
            LstItems.ItemsSource = null;
            LstItems.ItemsSource = _viewModel.CurrentItems;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnRemoveItem_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (sender is Button btn && btn.Tag is SaleItem item)
            {
                _viewModel.RemoveItem(item);
                UpdateTotal();
                LstItems.ItemsSource = null;
                LstItems.ItemsSource = _viewModel.CurrentItems;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при удалении товара: {ex.Message}", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnDeleteSale_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (sender is Button btn && btn.Tag is Sale sale)
            {
                if (MessageBox.Show($"Удалить продажу #{sale.Id} от {sale.SaleDate:dd.MM.yyyy HH:mm}?", 
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _data.DeleteSaleItemsBySaleId(sale.Id);
                    _data.DeleteSale(sale.Id);
                    _viewModel.LoadSales();
                    LstSales.ItemsSource = null;
                    LstSales.ItemsSource = _viewModel.Sales;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при удалении продажи: {ex.Message}", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _viewModel.SelectedEmployeeId = CmbEmployee.SelectedValue as int? ?? 0;
            _viewModel.NewSaleDate = DpDate.SelectedDate ?? DateTime.Now;
            
            if (CmbPayment.SelectedItem is ComboBoxItem paymentItem)
                _viewModel.NewPaymentMethod = paymentItem.Content?.ToString() ?? "Наличные";
            
            if (_viewModel.CompleteSale())
            {
                LstSales.ItemsSource = null;
                LstSales.ItemsSource = _viewModel.Sales;
                ClearForm();
                MessageBox.Show("Продажа успешно оформлена!", "Успех", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при оформлении продажи: {ex.Message}", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnClear_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.ClearForm();
        ClearForm();
    }

    private void UpdateTotal()
    {
        TxtTotal.Text = _viewModel.GetTotal().ToString("C");
    }

    private void ClearForm()
    {
        CmbEmployee.SelectedIndex = -1;
        DpDate.SelectedDate = DateTime.Now;
        CmbPayment.SelectedIndex = 0;
        CmbProduct.SelectedIndex = -1;
        TxtQuantity.Text = "1";
        LstItems.ItemsSource = null;
        LstItems.ItemsSource = _viewModel.CurrentItems;
        UpdateTotal();
    }
}