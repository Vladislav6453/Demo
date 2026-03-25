using System.Windows;
using System.Windows.Controls;
using Prototip.Models;
using Prototip.Services;
using Prototip.VM;

namespace Prototip.Views;

public partial class ProductsWindow : Window
{
    private readonly ProductsViewModel _viewModel;
    private readonly DataService _data = DataService.Instance;

    public ProductsWindow()
    {
        InitializeComponent();
        _viewModel = new ProductsViewModel();
        
        CmbCategory.ItemsSource = _viewModel.Categories;
        LstProducts.ItemsSource = _viewModel.Products;
        
        DataContext = _viewModel;
    }

    private void LstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LstProducts.SelectedItem is ProductViewModel selected)
        {
            var originalProduct = _data.GetProducts().FirstOrDefault(p => p.Id == selected.Id);
            if (originalProduct != null)
            {
                _viewModel.Select(originalProduct);
                
                TxtName.Text = _viewModel.NewName;
                TxtBarcode.Text = _viewModel.NewBarcode;
                CmbCategory.SelectedValue = _viewModel.NewCategoryId;
                TxtPurchasePrice.Text = _viewModel.NewPurchasePrice.ToString();
                TxtRetailPrice.Text = _viewModel.NewRetailPrice.ToString();
                TxtQuantity.Text = _viewModel.NewQuantity.ToString();
                
                foreach (ComboBoxItem item in CmbUnit.Items)
                {
                    if (item.Content?.ToString() == _viewModel.NewUnit)
                    {
                        CmbUnit.SelectedItem = item;
                        break;
                    }
                }
                
                TxtMinQuantity.Text = _viewModel.NewMinQuantity.ToString();
            }
        }
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.NewName = TxtName.Text;
        _viewModel.NewBarcode = TxtBarcode.Text;
        _viewModel.NewCategoryId = CmbCategory.SelectedValue as int? ?? 0;
        _viewModel.NewPurchasePrice = decimal.TryParse(TxtPurchasePrice.Text, out var p) ? p : 0;
        _viewModel.NewRetailPrice = decimal.TryParse(TxtRetailPrice.Text, out var r) ? r : 0;
        _viewModel.NewQuantity = int.TryParse(TxtQuantity.Text, out var q) ? q : 0;
        
        if (CmbUnit.SelectedItem is ComboBoxItem unitItem)
            _viewModel.NewUnit = unitItem.Content?.ToString() ?? "шт";
        else
            _viewModel.NewUnit = "шт";
            
        _viewModel.NewMinQuantity = int.TryParse(TxtMinQuantity.Text, out var m) ? m : 0;
        
        if (_viewModel.Add())
        {
            ClearForm();
        }
    }

    private void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.NewName = TxtName.Text;
        _viewModel.NewBarcode = TxtBarcode.Text;
        _viewModel.NewCategoryId = CmbCategory.SelectedValue as int? ?? 0;
        _viewModel.NewPurchasePrice = decimal.TryParse(TxtPurchasePrice.Text, out var p) ? p : 0;
        _viewModel.NewRetailPrice = decimal.TryParse(TxtRetailPrice.Text, out var r) ? r : 0;
        _viewModel.NewQuantity = int.TryParse(TxtQuantity.Text, out var q) ? q : 0;
        
        if (CmbUnit.SelectedItem is ComboBoxItem unitItem)
            _viewModel.NewUnit = unitItem.Content?.ToString() ?? "шт";
        else
            _viewModel.NewUnit = "шт";
            
        _viewModel.NewMinQuantity = int.TryParse(TxtMinQuantity.Text, out var m) ? m : 0;
        
        if (_viewModel.Update())
        {
            ClearForm();
        }
    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (_viewModel.Delete())
        {
            ClearForm();
        }
    }

    private void ClearForm()
    {
        TxtName.Clear();
        TxtBarcode.Clear();
        CmbCategory.SelectedIndex = -1;
        TxtPurchasePrice.Clear();
        TxtRetailPrice.Clear();
        TxtQuantity.Clear();
        CmbUnit.SelectedIndex = 0;
        TxtMinQuantity.Clear();
        LstProducts.SelectedItem = null;
    }
}